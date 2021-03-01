<template>
  <!-- Form thực hiện thêm sửa  -->
  <div class="comp-form">
    <form class="form">
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
            placeholder="Mã tài sản"
            :maxlength="maxAssetCode"
            @input="limitAssetCode"
            ref="code"
          />
        </div>
        <div class="form-group col-md-8">
          <label>Tên tài sản ( <span>*</span> )</label>
          <input
            type="text"
            class="input-search"
            v-model="dataItem.assetName"
            placeholder="Tên tài sản"
            :maxlength="maxAssetName"
            @input="limitAssetName"
            ref="name"
          />
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
            <option value="3f8e6896-4c7d-15f5-a018-75d8bd200d7c">CNTT</option>
            <option value="45ac3d26-18f2-18a9-3031-644313fbb055">HTKH</option>
            <option value="78aafe4a-67a7-2076-3bf3-eb0223d0a4f7">
              Finance
            </option>
            <option value="7c4f14d8-66fb-14ae-198f-6354f958f4c0">PDH</option>
          </select>
        </div>
        <div class="form-group col-md-8">
          <label>Tên phòng ban</label>
          <input
            type="text"
            class="input-search"
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
            ref="department"
          >
            <option value="1731fa87-79fd-4cc1-6978-553c0310877a">
              LTS5072
            </option>
            <option value="185f84ed-4563-51a0-cac7-6c0aeb6ec302">
              LTS7505
            </option>
            <option value="19165ed7-212e-21c4-0428-030d4265475f">
              LTS3847
            </option>
            <option value="27366e4a-5248-12e3-e312-ebbbcf065d1c">
              LTS1865
            </option>
            <option value="2924c34d-68f1-1d0a-c9c7-6c0aeb6ec302">
              LTS5663
            </option>
            <option value="3631011e-4559-4ad8-b0ad-cb989f2177da">
              LTS4560
            </option>
            <option value="471530a2-44fe-7395-b1ad-cb989f2177da">
              LTS0530
            </option>
            <option value="4cf2dd43-5f4b-71b6-e212-ebbbcf065d1c">
              LTS2240
            </option>
            <option value="5f7b48e5-16f9-2f2f-ecdc-845b5dcdad45">
              LTS2643
            </option>
            <option value="7a0b757e-41eb-4df6-c6f8-494a84b910f4">
              LTS6927
            </option>
          </select>
        </div>
        <div class="form-group col-md-8">
          <label>Tên loại tài sản</label>
          <input
            type="text"
            class="input-search"
            v-model="dataItem.assetTypeName"
            disabled
          />
        </div>
      </div>
      <div class="form-row">
          <div class="form-group col-md-4 ">
            <label >Ngày ghi tăng</label>
            <input
              type="DATE"
              class="input-search inputNumber"
              v-model="dataItem.increaseDate"
            />
          </div>
          <div class="form-group col-md-4 ">
            <label>Thời gian sử dung (năm)</label>
            <input
              type="text"
              class="input-search inputNumber"
              placeholder="thời gian sử dụng"
              v-model="dataItem.timeUse"
              @input="regextNumber(dataItem.timeUse)"
            />
          </div>
          <div class="form-group col-md-4 ">
            <label>Tỷ lệ hao mòn (%)</label>
            <input
              type="text"
              class="input-search inputNumber"
              placeholder="Tỷ lệ hao mòn"
              v-model="dataItem.wearRate"
              @input="regextNumber(dataItem.wearRate)"
            />
          </div>
      </div>
      <div class="form-row">
          <div class="form-group col-md-4 ">
            <label>Nguyên giá</label>
            <input
              type="text"
              class="input-search inputNumber"
              v-model="dataItem.originalPrice"
              placeholder="Nguyên giá"
              @input="regextNumber(dataItem.originalPrice)"
            />
          </div>
          <div class="form-group col-md-4 ">
            <label>Giá trị hao mòn năm</label>
            <input
              type="text"
              class="input-search inputNumber"
              placeholder="Giá trị hao mòn"
              v-model="dataItem.wearValue"
              @input="regextNumber(dataItem.wearValue)"
            />
          </div>
      </div>
      <footer>
        <!-- Khi click vào nút hủy tắt form và reset dữ liệu  -->
        <button class="btn-add btn-cancer" @click.prevent="showOffForm">
          Hủy
        </button>
        <!-- Validate dữ liệu trên form rồi kiểm tra xem là thêm hay sửa  -->
        <button class="btn-add" @click.prevent="addDataAsset">Lưu</button>
      </footer>
    </form>
    <notifications group="foo" />
  </div>
</template>

<script>
import * as axios from "axios";
export default {
  props: {
    dataAsset: Array, // Mảng tất cả dữ liệu truyền từ Comp-list xuống
    itemTemp: Object, // Dữ liệu 1 đối tượng để truyền vào form
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
        wearValue: 0,
      },
      maxAssetCode: 50, // giới hạn kí tự nhập vào ô mã tài sản
      maxAssetName: 255, // giới hạn kí tự nhập vào ô tên tài sản
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
    this.dataItem.assetTypeId = this.itemTemp.assetTypeId;
    this.dataItem.assetTypeName = this.itemTemp.assetTypeName;
    this.dataItem.departmentCode = this.itemTemp.departmentCode;
    this.dataItem.departmentId = this.itemTemp.departmentId;
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
    this.dataAsset.forEach((element) => {
      if (element.assetTypeId == this.dataItem.assetTypeId) {
        this.dataItem.assetTypeName = element.assetTypeName;
      }
      if (element.departmentId == this.dataItem.departmentId) {
        this.dataItem.departmentName = element.departmentName;
      }
    });
  },
  methods: {
    /**
     * Format lại giá trên form
     */
    formatPrice(value) {
      if (value != null) {
        let val = (value / 1).toFixed(0).replace(".", ",");
        return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
      }
      return value;
    },
    /**
     * giới hạn kí tự khi nhập vào input mã
     */
    limitAssetCode() {
      if (this.dataItem.assetCode != null) {
        if (this.dataItem.assetCode.length == this.maxAssetCode) {
          this.$notify({
            group: "foo",
            title: "Cảnh báo",
            text:
              "Mã tài sản không được nhập quá " + this.maxAssetCode + " kí tự",
            type: "error",
          });
        }
      }
      this.regexText();
    },
    /**
     * Validate không được nhập kí tự đặc biệt
     */
    regexText() {
      var format = "<>@!#$%^&*()_+[]{}?:;|'\"\\,./~`=";
      for (let i = 0; i < format.length; i++) {
        if (this.dataItem.assetCode.indexOf(format[i]) > -1) {
          this.$notify({
            group: "foo",
            title: "Cảnh báo",
            text: "Không được nhập kí tự đặc biệt",
            type: "error",
          });
        }
      }
    },
    /**
     * giới hạn kí tự khi nhập vào input tên
     */
    limitAssetName() {
      if (this.dataItem.assetName != null) {
        if (this.dataItem.assetName.length == this.maxAssetName) {
          this.$notify({
            group: "foo",
            title: "Cảnh báo",
            text:
              "Tên tài sản không được nhập quá " + this.maxAssetName + " kí tự",
            type: "error",
          });
        }
      }
      this.regexText();
    },
    regextNumber(value) {
      var numbers = /^[0-9]+$/;
      if (!value.match(numbers) && value != "") {
        this.$notify({
          group: "foo",
          title: "Cảnh báo",
          text: "Trường này chỉ được nhập số",
          type: "error",
        });
      }
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
      if (this.validateData.error) {
        this.$notify({
          group: "foo",
          title: "Cảnh báo",
          text: this.validateData.msg,
          type: "error",
        });
        switch (this.validateData.typeError) {
          case "code":
            this.$refs.code.focus();
            break;
          case "name":
            this.$refs.name.focus();
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
        if (this.dataItem.increaseDate == "") {
          this.dataItem.increaseDate = null;
        }
        //  debugger // eslint-disable-line
        if (this.dataItem.assetId == null) {
          // debugger // eslint-disable-line
          // Thực hiện post
          const response = axios
            .post("http://localhost:51888/api/v1/Assets", this.dataItem)
            .catch((e) => console.log(e));
          console.log(response);
          this.$notify({
            group: "foo",
            title: "Thành công",
            text: "Thêm mới thành công",
            type: "success",
          });
        } else {
          // Thực hiện put
          let apiUrl =
            "http://localhost:51888/api/v1/Assets/" + this.dataItem.assetId;
          const response = axios
            .put(apiUrl, this.dataItem)
            .catch((e) => console.log(e));
          console.log(response);
          this.$notify({
            group: "foo",
            title: "Thành công",
            text: "Cập nhật thành công",
            type: "success",
          });
        }
        // this.$emit("addAsset",this.dataItem);
        this.showOffForm();
        location.reload();
      }
    },
  },
  computed: {
    /**
     * Validate dữ liệu trên form
     */
    validateData() {
      // debugger // eslint-disable-line
      let returnData = {
        error: false,
        msg: "",
        typeError: "",
      };

      //1. validate để trống
      //Để trống loại tài sản
      if (this.dataItem.assetTypeId == null) {
        returnData = {
          error: true,
          msg: "Vui lòng chọn loại tài sản",
          typeError: "type",
        };
      }
      // Để trống tên phòng ban
      if (this.dataItem.departmentId == null) {
        returnData = {
          error: true,
          msg: "Vui lòng chọn phòng ban tài sản",
          typeError: "department",
        };
      }
      // Để trống tên tài sản
      if (this.dataItem.assetName == null || this.dataItem.assetName == "") {
        returnData = {
          error: true,
          msg: "Vui lòng nhập tên tài sản",
          typeError: "name",
        };
      }
      // Để trống mã tài sản
      if (this.dataItem.assetCode == null || this.dataItem.assetCode == "") {
        returnData = {
          error: true,
          msg: "Vui lòng nhập mã tài sản",
          typeError: "code",
        };
      }
      // 2.validate trùng mã tài sản
      // if (this.dataItem.assetId == null) {
      this.dataAsset.forEach((element) => {
        if (
          this.dataItem.assetCode == element.assetCode &&
          this.dataItem.assetId != element.assetId
        ) {
          returnData = {
            error: true,
            msg: "Mã tài sản đã tồn tại vui lòng kiểm tra lại",
            typeError: "code",
          };
        }
      });
      // }

      return returnData;
    },
  },
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
.input-search {
  font-family: "GoogleSans-Thin";
  font-size: 13px;
  height: 35px;
  width: 100%;
}
.form-row {
    margin-left: 20px;
    margin-right: 20px;
}
/* button cho footer form */
button.btn-add {
  background-color: #00abfe;
  color: white;
}
button.btn-cancer {
  background-color: white;
  color: black;
  border: 1px solid #e2e2e2;
}
.icon-header img {
  margin-left: 10px;
}
</style>