import Vue from 'vue'
import VueRouter from 'vue-router'
import InstitutionStructureForm from '../views/InstitutionStructureForm.vue'
import InstitutionStructure from '../views/InstitutionStructure.vue'
import Diagram from '../views/Diagram.vue'
import PageNotFound from '../views/PageNotFound.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/InstitutionStructure/:id',
    name: 'InstitutionStructure',
    component: InstitutionStructure
  },
  {
    path: '/InstitutionStructureForm/:id/:idRecord',
    name: 'InstitutionStructureForm',
    component: InstitutionStructureForm
  },
  {
    path: '/Diagram/:id',
    name: 'Diagram',
    component: Diagram
  },
  {
    path: '*',
    name: 'PageNotFound',
    component: PageNotFound
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
