<template>
  <!-- Danh sách tài sản  -->
  <div class="content-list">
    <!-- Mục tìm kiếm và thêm sửa xóa item  -->
    <div class="filter-bar">
      <div class="filter-left">
        <input
          id="txtSearch"
          class="icon-search input-search"
          type="text"
          placeholder="Tìm kiếm"
          v-model="textSearch"
        />
        <select
          name="departmentCode"
          id="departmentCode"
          class="input-search dropbox-filter"
          v-model="departmentFilter"
        >
          <option value="">Tất cả phòng ban</option>
          <option
            v-for="(department, index) in departments"
            :key="index"
            :value="department.departmentId"
          >
            {{ department.departmentName }}
          </option>

          <!-- <option value="3f8e6896-4c7d-15f5-a018-75d8bd200d7c">
            Information Technology
          </option>
          <option value="45ac3d26-18f2-18a9-3031-644313fbb055">
            Customer Support
          </option>
          <option value="78aafe4a-67a7-2076-3bf3-eb0223d0a4f7">Finance</option>
          <option value="7c4f14d8-66fb-14ae-198f-6354f958f4c0">
            Executive
          </option> -->
        </select>
        <select
          name="assetTypeCode"
          id="assetTypeCode"
          class="input-search dropbox-filter"
          v-model="assetTypeFilter"
        >
          <option value="">Tất cả Loại tài sản</option>
          <option
            v-for="(type, index) in assetTypes"
            :key="index"
            :value="type.assetTypeId"
          >
            {{ type.assetTypeName }}
          </option>
          <!-- <option value="1731fa87-79fd-4cc1-6978-553c0310877a">toan6</option>
          <option value="185f84ed-4563-51a0-cac7-6c0aeb6ec302">
            Abreu1984
          </option>
          <option value="19165ed7-212e-21c4-0428-030d4265475f">
            Edmonds2029
          </option>
          <option value="27366e4a-5248-12e3-e312-ebbbcf065d1c">boy862</option>
          <option value="2924c34d-68f1-1d0a-c9c7-6c0aeb6ec302">
            Swain2004
          </option>
          <option value="3631011e-4559-4ad8-b0ad-cb989f2177da">tien772</option>
          <option value="471530a2-44fe-7395-b1ad-cb989f2177da">ma164</option>
          <option value="4cf2dd43-5f4b-71b6-e212-ebbbcf065d1c">
            Dudley1981
          </option>
          <option value="5f7b48e5-16f9-2f2f-ecdc-845b5dcdad45">
            Watson1986
          </option>
          <option value="7a0b757e-41eb-4df6-c6f8-494a84b910f4">huong9</option> -->
        </select>
      </div>
      <div class="filter-right">
        <button type="button" class="btn-add" @click="showForm">Thêm</button>
        <!-- Icon refresh  -->
        <img :src="refreshIcon" alt="refresh" @click="reRender" />
        <!-- Icon delete  -->
        <img :src="deleteIcon" alt="delete" @click="isdeleteAllItem" />
      </div>
    </div>
    <!-- Mục danh sách item  -->
    <div class="table-data" :key="componentKey">
      <table class="table">
        <thead>
          <tr>
            <th v-if="isCheckbox" width="1%">
              <img :src="deleteIcon" alt="delete" @click="deletesAsset" />
            </th>
            <th width="3%" class="order">STT</th>
            <th width="10%" class="increaseDate">NGÀY GHI TĂNG</th>
            <th width="10%">MÃ TÀI SẢN</th>
            <th width="30%">TÊN TÀI SẢN</th>
            <th width="12%">LOẠI TÀI SẢN</th>
            <th width="15%">PHÒNG BAN</th>
            <th width="12%" class="originalPrice">NGUYÊN GIÁ</th>
            <th width="8%">CHỨC NĂNG</th>
          </tr>
        </thead>
        <tbody v-if="assets.length > 0">
          <!-- Duyệt mảng rồi render ra các item  -->
          <!-- khi click vào item -> gán biến isActive bằng giá trị index để hiển thị active  -->
          <tr
            v-for="(item, index) in filteredAsset"
            :key="index"
            :class="{ highlight: isActive == index }"
            @click="activeItem(index)"
          >
            <td v-if="isCheckbox">
              <input
                type="checkbox"
                id="vehicle1"
                name="vehicle1"
                :value="item"
                v-model="idDeletes"
              />
            </td>
            <th class="order">{{ index + 1 }}</th>
            <td class="increaseDate">
              {{ format_date(item.increaseDate) }}
            </td>
            <td>{{ item.assetCode }}</td>
            <td>{{ item.assetName }}</td>
            <td>{{ item.assetTypeName }}</td>
            <td>{{ item.departmentName }}</td>
            <td class="originalPrice">
              {{ formatPrice(item.originalPrice) }}
            </td>
            <td class="fuctionCol">
              <div class="editIcon" @click="getItem(item)" title="Sửa"></div>
              <div
                class="deleteIcon"
                @click="showPopupDelete(item)"
                title="Xóa"
              ></div>
              <div
                class="duplicateIcon"
                @click="duplicateItem(item)"
                title="Sao chép tài sản này"
              ></div>
            </td>
          </tr>
        </tbody>
        <div v-if="assets.length == 0" class="notData">
          <h1>Không có dữ liệu</h1>
        </div>
      </table>
    </div>
    <!-- Hiển thị Popup khi isPopup = true , ẩn khi isPopup = false  -->
    <transition name="slide-fade">
      <div class="popup" v-if="isPopup">
        <div class="popupNotify">
          <h3>Xác nhận</h3>
          <p>Bạn chắc chắn muốn xóa tài sản này ?</p>
          <div class="btn-popup">
            <button class="btn-add btn-cancel" @click="offPopupDelete">
              Hủy
            </button>
            <button class="btn-add btn-del" @click="deleteItem">Xóa</button>
          </div>
        </div>
      </div>
    </transition>
    <!-- Hiển thị form khi isForm = true , ẩn khi isForm = false  -->
    <component-form
      v-if="isForm"
      :dataAsset="assets"
      :dataAssetTypes="assetTypes"
      :dataDepartments="departments"
      :itemTemp="itemTemp"
      @resetItem="resetItem"
    />
    <!-- Mục thống kê tổng  -->
    <footer>
      <p>Tổng số tài sản : {{ assets.length }}</p>
      <p class="sum-asset">Tổng nguyên giá: {{ formatPrice(sumPrice) }}</p>
    </footer>
    <notifications group="foo" />
  </div>
</template>

<script>
import * as axios from "axios";
import moment from "moment";
import ComponentForm from "./ComponentForm.vue";
export default {
  components: {
    ComponentForm,
  },
  data() {
    return {
      refreshIcon: require("../../assets/icon/refresh.svg"),
      deleteIcon: require("../../assets/icon/trash.svg"),
      // Dữ liệu lấy về từ api
      assets: [],
      isActive: 0, // lưu item đang được trỏ tới
      componentKey: 0, // Biến refresh table
      isCheckbox: false, // Hiển thị checkbox
      itemTemp: {}, // Data 1 tài sản đẩy lên form
      textSearch: "", // lưu trữ kí tự tìm kiếm
      idDeletes: [], // lưu id delete
      departmentFilter: "", // id lọc phòng ban
      assetTypeFilter: "", // id lọc loại
      isPopup: false, // đóng mở popup thông báo xóa
      assetTypes: [], // Dữ liệu loại tài sản
      departments: [], // Dữ liệu phòng ban
    };
  },
  methods: {
    // keyCodeWindow(e){
    //   if(e.keyCode == 38 && this.isActive > 0){
    //     this.isActive --;
    //   }
    //   if(e.keyCode == 40 && this.isActive < this.assets.length - 1){
    //     this.isActive ++;
    //   }
    // },
    /**
     * gán giá trị isActive bằng index để khi click vào item nào chuyển active đến item đó
     */
    activeItem(index) {
      this.isActive = index;
    },
    /**
     * Reset lại bảng dữ liệu
     */
    reRender() {
      // this.componentKey += 1;
      // this.textSearch = "";
      // this.departmentFilter = "";
      // this.assetTypeFilter = "";
      location.reload();
    },
    /**
     * Bật checkbox để xáo nhiều item
     * Khi tắt checkbox bỏ tích các checkbox
     */
    isdeleteAllItem() {
      this.isCheckbox = !this.isCheckbox;
      if (this.isCheckbox == false) {
        this.idDeletes = [];
      }
    },
    /**
     * Hiển thị Form
     */
    showForm() {
      this.$store.dispatch("onForm");
    },
    /**
     * Sửa lại giá trị ngày tháng rồi đẩy dữ liệu lên form edit
     */
    getItem(item) {
      if (item.increaseDate != null) {
        item.increaseDate = item.increaseDate.substring(0, 10);
      }
      this.itemTemp = item;
      this.showForm();
    },
    /**
     * Nhân bản object đẩy lên form thêm
     * Để trống Tên, Mã, Id
     */
    duplicateItem(item) {
      if (item.increaseDate != null) {
        item.increaseDate = item.increaseDate.substring(0, 10);
      }
      this.itemTemp.assetTypeCode = item.assetTypeCode;
      this.itemTemp.assetTypeId = item.assetTypeId;
      this.itemTemp.assetTypeName = item.assetTypeName;
      this.itemTemp.departmentCode = item.departmentCode;
      this.itemTemp.departmentId = item.departmentId;
      this.itemTemp.departmentName = item.departmentName;
      this.itemTemp.increaseDate = item.increaseDate;
      this.itemTemp.isUsed = item.isUsed;
      this.itemTemp.originalPrice = item.originalPrice;
      this.itemTemp.timeUse = item.timeUse;
      this.itemTemp.wearRate = item.wearRate;
      this.itemTemp.wearValue = item.wearValue;
      this.$store.dispatch("onForm");
    },
    /**
     * Đưa dữ liệu đẩy lên form về rỗng
     * để khi click vào insert không còn dữ liệu
     */
    resetItem() {
      this.itemTemp = {};
    },
    /**
     * Format lại ngày tháng năm
     */
    format_date(value) {
      if (value) {
        return moment(String(value)).format("DD-MM-YYYY");
      }
    },
    /**
     * Format giá từ dạng 1000 -> 1.000
     */
    formatPrice(value) {
      if (value != null) {
        let val = (value / 1).toFixed(0).replace(".", ",");
        return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
      }
    },
    /**
     * click vào icon xóa hiện ra popup
     */
    showPopupDelete(item) {
      this.isPopup = true;
      this.itemDelete = item;
    },
    showPopupDeletes() {
      this.isPopup = true;
    },
    /**
     * click vào icon xóa hiện ra popup
     */
    offPopupDelete() {
      this.isPopup = false;
      this.itemDelete = {};
    },
    /**
     * Xóa 1 object trong database ảo
     * Call api xóa trên database thực
     */
    deleteItem() {
      if (this.idDeletes.length == 0) {
        // xóa trong mảng
        this.assets.splice(this.assets.indexOf(this.itemDelete), 1);
        // call api xóa trên database
        let apiUrl =
          "http://localhost:51888/api/v1/Assets/" + this.itemDelete.assetId;
        const response = axios.delete(apiUrl).catch((e) => console.log(e));
        console.log(response);
      } else {
        var listDeletes = "";
        this.idDeletes.forEach((element) => {
          listDeletes += "," + element.assetId;
        });
        this.idDeletes.forEach((item) => {
          this.assets.splice(this.assets.indexOf(item), 1);
        });
        let apiUrl =
          "http://localhost:51888/api/v1/Assets?param=" +
          listDeletes.substring(1);
        // call api xóa nhiều tài sản trên database
        const response = axios.delete(apiUrl).catch((e) => console.log(e));
        console.log(response);
        this.$forceUpdate();
      }
      this.offPopupDelete();
    },
    /**
     * Xóa nhiều tài sản cùng 1 lúc
     */
    deletesAsset() {
      if (this.idDeletes.length == 0) {
        // alert("Bạn chưa chọn tài sản xóa");
        this.$notify({
          group: "foo",
          title: "Cảnh báo",
          text: "Bạn chưa chọn tài sản xóa!",
          type: "error",
        });
      } else {
        this.showPopupDeletes();
      }
    },
  },
  computed: {
    /**
     * Ânr hiện form
     */
    isForm() {
      return this.$store.state.isForm;
    },
    /**
     * Tính tổng giá tiền tài sản
     */
    sumPrice() {
      var sum = 0;
      for (let i = 0; i < this.assets.length; i++) {
        sum += this.assets[i].originalPrice;
      }
      return sum;
    },
    /**
     * Hàm lọc giá trị theo ô tìm kiếm (texsearch)
     * Lọc theo phòng ban (departmentFilter)
     * Lọc theo loại tài sản (assetTypeFilter)
     */
    filteredAsset() {
      // debugger // eslint-disable-line
      let filterAssetType = this.assetTypeFilter,
        filterDepartment = this.departmentFilter,
        filterText = this.textSearch;

      return this.assets.filter(function (item) {
        let filtered = true;
        if (filterAssetType && filterAssetType.length > 0) {
          filtered = item.assetTypeId == filterAssetType;
        }
        if (filtered) {
          if (filterDepartment && filterDepartment.length > 0) {
            filtered = item.departmentId == filterDepartment;
          }
        }
        if (filtered) {
          if (filterText && filterText.length > 0) {
            filtered =
              item.assetCode.toLowerCase().includes(filterText.toLowerCase()) ||
              item.assetName.toLowerCase().includes(filterText.toLowerCase());
          }
        }
        return filtered;
      });
    },
  },
  // call api lấy toàn bộ dữ liệu tài sản
  async created() {
    const assets = await axios.get("http://localhost:51888/api/v1/Assets");
    this.assets = assets.data;

    const assetType = await axios.get(
      "http://localhost:51888/api/v1/AssetTypes"
    );
    this.assetTypes = assetType.data;

    const department = await axios.get(
      "http://localhost:51888/api/v1/Departments"
    );
    this.departments = department.data;

    // window.addEventListener("keyup" , this.keyCodeWindow);
  },
};
</script>

<style scoped>
/*------------------------------*/
#txtSearch {
  min-width: 400px;
}
.content-list {
  margin: 15px 20px;
}

.filter-bar {
  display: flex;
  justify-content: space-between;
  margin-bottom: 30px;
}
.filter-right {
  margin-right: 20px;
}
.filter-right img {
  border: 1px solid #e2e2e2;
  color: #e2e2e2;
  padding: 8px;
  margin-left: 10px;
  border-radius: 5px;
}
.filter-right img:hover {
  transform: scale(1.1);
}
.icon-search {
  background-image: url("../../assets/search.png");
  background-repeat: no-repeat;
  background-position: 375px center;
}
footer {
  display: flex;
  justify-content: space-between;
  padding-top: 20px;
  font-size: 14px;
  font-weight: 400;
  position: fixed;
  bottom: 0;
  width: 100%;
  background-color: white;
}
p.sum-asset {
  margin-right: 130px;
}
.originalPrice {
  text-align: right;
  padding-right: 15px;
}
/* Vẽ icon cho phần chức năng */
.editIcon {
  background: url("../../assets/icon.png") no-repeat -15px -15px;
  width: 15px;
  height: 16px;
}
.deleteIcon {
  background: url("../../assets/icon.png") no-repeat -43px -14px;
  width: 14px;
  height: 15px;
}
.duplicateIcon {
  background: url("../../assets/icon.png") no-repeat -70px -14px;
  width: 16px;
  height: 15px;
}
td.fuctionCol {
  display: flex;
  border-bottom: none;
  padding: 7px;
}
.fuctionCol div {
  margin: 7px;
  opacity: 0;
  visibility: hidden;
}
/* khi hover vào hiển thị chức năng */
tbody tr:hover .fuctionCol div {
  opacity: 1;
  visibility: visible;
}
/*item nào được active thì hiển thị cột chức năg */
.highlight .fuctionCol div {
  opacity: 1;
  visibility: visible;
}
.dropbox-filter {
  margin-left: 10px;
  font-family: "GoogleSans-Thin";
}
/** 
  Căn giữa ngày tháng
*/
.increaseDate {
  text-align: center;
}
/**
Nút hủy
 */
button.btn-add.btn-cancel {
  background-color: white;
  color: black;
}
/**
Nếu không có dữu liệu
 */
.notData {
    width: 100%;
    position: absolute;
    margin-top: 20px;
    margin-left: 20px;
}
.notData h1 {
    font-size: 25px;
}
.order{
  border-left: none;
  text-align: center;
}
</style>