<template>
  <!-- Form thực hiện thêm sửa  -->
  <div class="comp-form">
    <form class="form" @submit="submitForm">
      <!-- phần header có title và 2 icon help và close  -->
      <div class="header-form">
        <p>Ghi tăng giá trị</p>
        <div class="icon-header">
          <img :src="helpIcon" alt="icon" />
          <img :src="closeIcon" alt="icon" @click="showOffForm" />
        </div>
      </div>
      <div class="form-row">
        <div class="form-group col-md-4">
          <label>Mã tài sản ( <span>*</span> )</label>
          <input
            type="text"
            class="input-search"
            v-model="dataItem.assetCode"
            :maxlength="maxAssetCode"
            @input="limitAssetCode"
            ref="code"
            :class="{ warnning: isCheckCode }"
          />
          <p v-if="isCheckCode" class="textWarning">
            {{ msgCode }}
          </p>
        </div>
        <div class="form-group col-md-8">
          <label>Tên tài sản ( <span>*</span> )</label>
          <input
            type="text"
            class="input-search"
            v-model="dataItem.assetName"
            :maxlength="maxAssetName"
            @input="limitAssetName"
            ref="name"
            :class="{ warnning: isCheckName }"
          />
          <p v-if="isCheckName" class="textWarning">
            {{ msgName }}
          </p>
        </div>
      </div>
      <div class="form-row">
        <div class="form-group col-md-4">
          <label>Mã Phòng Ban</label>
          <select
            name="departmentCode"
            id="departmentCode"
            class="input-search"
            v-model="dataItem.departmentId"
            ref="department"
          >
            <option value="" disabled>
              Chọn mã Phòng ban
            </option>
            <option
              v-for="(department, index) in dataDepartments"
              :key="index"
              :value="department.departmentId"
            >
              {{ department.departmentName }}
            </option>
          </select>
        </div>
        <div class="form-group col-md-8">
          <label>Tên phòng ban</label>
          <input
            type="text"
            class="input-search input-disabled"
            v-model="dataItem.departmentName"
            disabled
          />
        </div>
      </div>
      <div class="form-row">
        <div class="form-group col-md-4">
          <label>Mã loại tài sản</label>
          <select
            name="assetTypeCode"
            id="assetTypeCode"
            class="input-search"
            v-model="dataItem.assetTypeId"
            ref="type"
          >
            <option value="" disabled>
              Chọn mã tài sản
            </option>
            <option
              v-for="(type, index) in dataAssetTypes"
              :key="index"
              :value="type.assetTypeId"
            >
              {{ type.assetTypeName }}
            </option>
          </select>
        </div>
        <div class="form-group col-md-8">
          <label>Tên loại tài sản</label>
          <input
            type="text"
            class="input-search input-disabled"
            v-model="dataItem.assetTypeName"
            disabled
          />
        </div>
      </div>
      <div class="form-row">
        <div class="form-group col-md-4">
          <label>Ngày ghi tăng</label>
          <date-picker
            v-model="dataItem.increaseDate"
            value-type="YYYY-MM-DD"
            format="DD-MM-YYYY"
            class="datepicker"
            placeholder="dd-mm-yyyy"
          ></date-picker>
          <!-- <input
            type="date"
            class="input-search inputNumber"
            v-model="dataItem.increaseDate"
          /> -->
        </div>
        <div class="form-group col-md-4">
          <label>Thời gian sử dung (năm)</label>
          <input
            type="text"
            class="input-search inputNumber"
            v-model="dataItem.timeUse"
            @input="timeUseNumber"
            :maxlength="maxNumber"
          />
        </div>
        <div class="form-group col-md-4">
          <label>Tỷ lệ hao mòn (%)</label>
          <input
            type="text"
            class="input-search inputNumber"
            v-model="dataItem.wearRate"
            @input="wearRateNumber"
            :maxlength="maxNumber"
          />
        </div>
      </div>
      <div class="form-row">
        <div class="form-group col-md-4">
          <label>Nguyên giá</label>

          <input
            type="text"
            class="input-search inputNumber"
            v-model="dataItem.originalPrice"
            @input="originalPriceNumber"
            :maxlength="maxPrice"
          />
        </div>
        <div class="form-group col-md-4">
          <label>Giá trị hao mòn năm</label>
          <input
            type="text"
            class="input-search inputNumber"
            v-model="dataItem.wearValue"
            @input="wearValueNumber"
            :maxlength="maxNumber"
          />
        </div>
      </div>
      <footer>
        <!-- Khi click vào nút hủy tắt form và reset dữ liệu  -->
        <button class="btn-add btn-cancel" @click.prevent="showOffForm">
          Hủy
        </button>
        <!-- Validate dữ liệu trên form rồi kiểm tra xem là thêm hay sửa  -->
        <button class="btn-add btn-submit" @click.prevent="addDataAsset">
          Lưu
        </button>
      </footer>
    </form>
    <notifications group="foo" />
  </div>
</template>

<script>
import * as axios from "axios";
import DatePicker from "vue2-datepicker";
import "vue2-datepicker/index.css";

export default {
  components: { DatePicker },
  props: {
    dataAsset: Array, // Mảng tất cả dữ liệu truyền từ Comp-list xuống
    dataDepartments: Array, // Mảng dữ liệu phòng ban truyền từ Comp-list xuống
    dataAssetTypes: Array, // Mảng dữ liệu loại tài sản truyền từ Comp-list xuống
    itemTemp: Object // Dữ liệu 1 đối tượng để truyền vào form
  },
  data() {
    return {
      // Khai bao svg
      helpIcon: require("../../assets/icon/help.svg"),
      closeIcon: require("../../assets/icon/cancel.svg"),
      // Dữ liệu item đẩy lên form
      dataItem: {
        assetCode: "",
        assetId: "",
        assetName: "",
        assetTypeCode: "",
        assetTypeId: "",
        assetTypeName: "",
        departmentCode: "",
        departmentId: "",
        departmentName: "",
        increaseDate: "",
        isUsed: 0,
        originalPrice: 0,
        timeUse: 0,
        wearRate: 0,
        wearValue: 0
      },
      maxAssetCode: 50, // giới hạn kí tự nhập vào ô mã tài sản
      maxAssetName: 255, // giới hạn kí tự nhập vào ô tên tài sản
      maxNumber: 9, // giới hạn các giá trị nhập số
      maxPrice: 12, // giới hạn nguyên giá
      isCheckCode: false, // thông báo lỗi khi không nhập mã hoặc trùng mã
      isCheckName: false, // thông báo khi không nhập tên
      msgCode: "", // tên nhắn lỗi
      msgName: "" // tin nhắn lỗi
    };
  },
  /**
   * gán giá trị item vào form sau khi render
   */
  mounted() {
    this.dataItem.assetCode = this.itemTemp.assetCode;
    this.dataItem.assetId = this.itemTemp.assetId;
    this.dataItem.assetName = this.itemTemp.assetName;
    this.dataItem.assetTypeCode = this.itemTemp.assetTypeCode;
    this.dataItem.assetTypeId =
      this.itemTemp.assetTypeId != null ? this.itemTemp.assetTypeId : "";
    this.dataItem.assetTypeName = this.itemTemp.assetTypeName;
    this.dataItem.departmentCode = this.itemTemp.departmentCode;
    this.dataItem.departmentId =
      this.itemTemp.departmentId != null ? this.itemTemp.departmentId : "";
    this.dataItem.departmentName = this.itemTemp.departmentName;
    this.dataItem.increaseDate = this.itemTemp.increaseDate;
    this.dataItem.isUsed = this.itemTemp.isUsed;
    this.dataItem.originalPrice = this.formatPrice(this.itemTemp.originalPrice);
    this.dataItem.timeUse = this.itemTemp.timeUse;
    this.dataItem.wearRate = this.itemTemp.wearRate;
    this.dataItem.wearValue = this.itemTemp.wearValue;
  },
  /**
   * Lifecycle gán trước khi update thực hiện khi thay đổi mã thì thay đổi tên theo database
   */
  beforeUpdate() {
    this.dataAsset.forEach(element => {
      if (element.assetTypeId == this.dataItem.assetTypeId) {
        this.dataItem.assetTypeName = element.assetTypeName;
      }
      if (element.departmentId == this.dataItem.departmentId) {
        this.dataItem.departmentName = element.departmentName;
      }
    });
  },
  methods: {
    submitForm(e) {
      // To prevent the form from submitting
      e.preventDefault();
      // return false;
    },
    /**
     * Sự kiện nút tắt và lưu form
     */
    addKeyForm(e) {
      if (this.isForm) {
        if (e.which == 27) {
          this.showOffForm();
        }
        // if (e.which == 13) {
        //   this.addDataAsset();
        // }
      }
    },
    /**
     * Format lại giá trên form
     */
    formatPrice(value) {
      if (value != null) {
        // Làm tròn rồi chèn dấu .
        let val = (value / 1).toFixed(0).replace(".", ",");
        return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
      }
      return value;
    },
    /**
     * giới hạn kí tự khi nhập vào input mã
     */
    limitAssetCode() {
      // thông báo giới hạn kí tự
      if (this.dataItem.assetCode != null) {
        if (this.dataItem.assetCode.length == this.maxAssetCode) {
          this.$notify({
            group: "foo",
            title: "Cảnh báo",
            text:
              "Mã tài sản không được nhập quá " + this.maxAssetCode + " kí tự",
            type: "error"
          });
        }
        this.isCheckCode = false;
      }
      // Thông báo không được nhập kí tự đặc việt
      // Tự động replace
      var format = "<>@!#$%^&*()_+[]{}?:;|'\"\\,./~`= ";
      for (let i = 0; i < format.length; i++) {
        if (this.dataItem.assetCode.indexOf(format[i]) > -1) {
          this.isCheckCode = true;
          this.msgCode = "Không được nhập kí tự đặc biệt";
          this.dataItem.assetCode = this.dataItem.assetCode.replace(/[^A-Za-z0-9]/, "");
        }
      }
      // this.isCheckCode = false;
    },
    /**
     * giới hạn kí tự khi nhập vào input tên
     */
    limitAssetName() {
      // Thông báo giới hạn kí tự
      if (this.dataItem.assetName != null) {
        if (this.dataItem.assetName.length == this.maxAssetName) {
          this.$notify({
            group: "foo",
            title: "Cảnh báo",
            text:
              "Tên tài sản không được nhập quá " + this.maxAssetName + " kí tự",
            type: "error"
          });
        }
        this.isCheckName = false;
      }
      // Thông báo không được nhập kí tự đặc biệt
      // Tự động replace
      var format = "<>@!#$%^&*()_+[]{}?:;|'\"\\,./~`= ";
      for (let i = 0; i < format.length; i++) {
        if (this.dataItem.assetName.indexOf(format[i]) > -1) {
          this.isCheckName = true;
          this.msgName = "Không được nhập kí tự đặc biệt";
          this.dataItem.assetName = this.dataItem.assetName.replace(/[^A-Za-z0-9]/, "");
        }
      }
      
    },
    /**
     * Validate trường số : không được nhập chữ và max = 9
     * Thời gian sử dụng
     */
    timeUseNumber() {
      // Thông báo giới hạn kí tự
      if (this.dataItem.timeUse != null) {
        if (this.dataItem.timeUse.length == this.maxNumber) {
          this.$notify({
            group: "foo",
            title: "Cảnh báo",
            text:
              "Thởi gian sử dụng không được nhập quá " +
              this.maxNumber +
              " kí tự",
            type: "error"
          });
        }
      }
      // Thông báo không được nhập chữ
      // Tự động repalce
      var numbers = /^[0-9]+$/;
      if (
        !this.dataItem.timeUse.match(numbers) &&
        this.dataItem.timeUse != ""
      ) {
        this.$notify({
          group: "foo",
          title: "Cảnh báo",
          text: "Trường này chỉ được nhập số",
          type: "error"
        });
      }
      this.dataItem.timeUse = this.dataItem.timeUse.replace(/\D/g, "");
    },
    /**
     * Tỷ lệ hao mòn
     */
    wearRateNumber() {
      // Thông báo giới hạn kí tự
      if (this.dataItem.wearRate != null) {
        if (this.dataItem.wearRate.length == this.maxNumber) {
          this.$notify({
            group: "foo",
            title: "Cảnh báo",
            text:
              "Tỷ lệ hao mòn không được nhập quá " + this.maxNumber + " kí tự",
            type: "error"
          });
        }
      }
      // Thông báo không được nhập chữ
      // Tự động replace
      var numbers = "^[0-9.]+$";
      if (
        !this.dataItem.wearRate.match(numbers) &&
        this.dataItem.wearRate != ""
      ) {
        this.$notify({
          group: "foo",
          title: "Cảnh báo",
          text: "Trường này chỉ được nhập số",
          type: "error"
        });
      }
      this.dataItem.wearRate = this.dataItem.wearRate.replace(/\D/g, "");
    },
    /**
     * Nguyên giá
     * format lại giá trị trên form
     */
    originalPriceNumber() {
      // Thông báo giới hạn kí tự
      if (this.dataItem.originalPrice != null) {
        if (this.dataItem.originalPrice.length == this.maxPrice) {
          this.$notify({
            group: "foo",
            title: "Cảnh báo",
            text: "Nguyên giá không được vượt quá tỷ",
            type: "error"
          });
        }
      }
      // Thông báo không được nhập chũ
      // format giá và replace chữ
      var numbers = "^[0-9.]+$";
      if (
        !this.dataItem.originalPrice.match(numbers) &&
        this.dataItem.originalPrice != ""
      ) {
        this.$notify({
          group: "foo",
          title: "Cảnh báo",
          text: "Trường này chỉ được nhập số",
          type: "error"
        });
      }
      this.dataItem.originalPrice = this.dataItem.originalPrice
        .replace(/\D/g, "")
        .replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    },
    /**
     * Gía trị hao mòn
     */
    wearValueNumber() {
      // Thông báo giới hạn kí tự
      if (this.dataItem.wearValue != null) {
        if (this.dataItem.wearValue.length == this.maxNumber) {
          this.$notify({
            group: "foo",
            title: "Cảnh báo",
            text:
              "Gía trị hao mòn không được nhập quá " +
              this.maxNumber +
              " kí tự",
            type: "error"
          });
        }
      }
      // Thông báo không được nhập chữ
      var numbers = /^[0-9]+$/;
      if (
        !this.dataItem.wearValue.match(numbers) &&
        this.dataItem.wearValue != ""
      ) {
        this.$notify({
          group: "foo",
          title: "Cảnh báo",
          text: "Trường này chỉ được nhập số",
          type: "error"
        });
      }
      this.dataItem.wearValue = this.dataItem.wearValue.replace(/\D/g, "");
    },
    /**
     * Tắt form và xóa dữ liệu item
     */
    showOffForm() {
      this.$store.dispatch("offForm");
      this.$emit("resetItem");
    },
    /**
     * Click vào nút lưu thực hiện thêm data khi id null và sửa data khi id khác null
     */
    addDataAsset() {
      // kiểm tra nghiệp vụ nếu false thì thực hiện thêm hoặc sửa
      if (this.validateData.error) {
        this.$notify({
          group: "foo",
          title: "Cảnh báo",
          text: this.validateData.msg,
          type: "error"
        });
        // focus vào ô input
        switch (this.validateData.typeError) {
          case "code":
            this.$refs.code.focus();
            this.isCheckCode = true;
            this.msgCode = this.validateData.msg;
            break;
          case "name":
            this.$refs.name.focus();
            this.isCheckName = true;
            this.msgName = this.validateData.msg;
            break;
          case "department":
            this.$refs.department.focus();
            break;
          case "type":
            this.$refs.type.focus();
            break;
          default:
            break;
        }
      } else {
        //chuyển datetime từ "" -> null
        if (this.dataDate == "") {
          this.dataItem.increaseDate = null;
        }
        // format lại giá để đẩy lên db 1.000 -> 1000
        if (this.dataItem.originalPrice != null) {
          this.dataItem.originalPrice = this.dataItem.originalPrice
            .split(".")
            .join("");
        }
        //  debugger // eslint-disable-line
        if (this.dataItem.assetId == null) {
          // debugger // eslint-disable-line
          // Thực hiện post
          axios
            .post("http://localhost:51888/api/v1/Assets", this.dataItem)
            .then(response => {
              if (!response.data.success) {
                this.$notify({
                  group: "foo",
                  title: "Lỗi",
                  text: response.data.data.userMsg[0],
                  type: "error"
                });
              } else {
                this.$notify({
                  group: "foo",
                  title: "Thành công",
                  text: "Thêm mới thành công",
                  type: "success"
                });
                this.showOffForm();
                location.reload();
              }
              console.log(response);
            })
            .catch(error => {
              this.$notify({
                group: "foo",
                title: "Lỗi",
                text:
                  "Đã có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp",
                type: "error"
              });
              console.log(error);
            });
        } else {
          // Thực hiện put
          let apiUrl =
            "http://localhost:51888/api/v1/Assets/" + this.dataItem.assetId;
          axios
            .put(apiUrl, this.dataItem)
            .then(response => {
              if (!response.data.success) {
                this.$notify({
                  group: "foo",
                  title: "Lỗi",
                  text: response.data.data.userMsg[0],
                  type: "error"
                });
              } else {
                this.$notify({
                  group: "foo",
                  title: "Thành công",
                  text: "Cập nhật thành công",
                  type: "success"
                });
                this.showOffForm();
                location.reload();
              }
              console.log(response);
            })
            .catch(error => {
              this.$notify({
                group: "foo",
                title: "Lỗi",
                text:
                  "Đã có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp",
                type: "error"
              });
              console.log(error);
            });
        }
      }
    }
  },
  computed: {
    /**
     * Lấy biến isForm từ store
     */
    isForm() {
      return this.$store.state.isForm;
    },
    /**
     * Validate dữ liệu trên form
     */
    validateData() {
      // debugger // eslint-disable-line
      let returnData = {
        error: false,
        msg: "",
        typeError: ""
      };
      //1. validate để trống
      //Để trống loại tài sản
      if (this.dataItem.assetTypeId == "") {
        returnData = {
          error: true,
          msg: "Vui lòng chọn loại tài sản",
          typeError: "type"
        };
      }
      // Để trống tên phòng ban
      if (this.dataItem.departmentId == "") {
        returnData = {
          error: true,
          msg: "Vui lòng chọn phòng ban tài sản",
          typeError: "department"
        };
      }
      // Để trống tên tài sản
      if (this.dataItem.assetName == null || this.dataItem.assetName == "") {
        returnData = {
          error: true,
          msg: "Vui lòng nhập tên tài sản",
          typeError: "name"
        };
      }
      // Để trống mã tài sản
      if (this.dataItem.assetCode == null || this.dataItem.assetCode == "") {
        returnData = {
          error: true,
          msg: "Vui lòng nhập mã tài sản",
          typeError: "code"
        };
      }
      return returnData;
    }
  },
  created() {
    window.addEventListener("keyup", this.addKeyForm);
  }
};
</script>

<style scoped>
label {
  font-family: "GoogleSans-Thin";
  font-size: 13px;
  margin-bottom: 10px;
  font-weight: 100;
  margin-top: 10px;
}
.header-form {
  padding: 20px 20px 0 20px;
  display: flex;
  justify-content: space-between;
}
.icon-header img {
  margin-left: 10px;
}
.input-search {
  font-family: "GoogleSans-Thin";
  height: 35px;
  width: 100%;
}
.form-row {
  margin-left: 20px;
  margin-right: 20px;
}
.warnning {
  border: 1px solid red;
}
p.textWarning {
  font-size: 11px;
  margin-bottom: -10px;
  color: #e24949;
  margin-top: 5px;
  font-weight: 100;
}
.input-disabled{
  background-color: #f5f5f5;
  border: 1px solid #e8e8e8;
}
</style>