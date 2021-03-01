import Vue from 'vue'
import App from './App.vue'
import { store }  from './store/Store'
import Notifications from 'vue-notification'

Vue.use(Notifications)

new Vue({
  // khai báo store.
  store,
  render: h => h(App)
}).$mount('#app')
