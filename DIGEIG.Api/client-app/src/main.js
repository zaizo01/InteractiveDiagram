import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import VueTree from '@ssthouse/vue-tree-chart'
import VModal from 'vue-js-modal'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import Vuelidate from 'vuelidate'
import VueSweetalert2 from 'vue-sweetalert2';
import VueSingleSelect from "vue-single-select";
import Vue2Editor from "vue2-editor";

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import 'sweetalert2/dist/sweetalert2.min.css';
import "./assets/styles/custom.scss";

Vue.config.productionTip = false

Vue.use(BootstrapVue)
Vue.use(VModal)
Vue.use(IconsPlugin)
Vue.use(Vuelidate)
Vue.use(VueSweetalert2);
Vue.use(Vue2Editor);

Vue.component('vue-tree', VueTree);
Vue.component('vue-single-select', VueSingleSelect);

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
