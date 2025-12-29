import { createRouter, createWebHistory } from 'vue-router'
import { jwtDecode } from 'jwt-decode'
const Layout = () => import('@/views/Layout/LayoutIndex.vue')
const Home = () => import('@/views/Home/HomeIndex.vue')
const Account = () => import('@/views/Account/AccountIndex.vue')

const routes = [
  {
    path: '/',
    component: Layout,
    children: [
      {
        path: '/',
        component: Home,
      },
    ],
  },
  {
    path: '/login',
    component: Account,
  },
]
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
})

router.beforeEach((to, from, next) => {
  const token = localStorage.getItem('jwtToken')
  const whiteList = ['/login', '/register']
  if (whiteList.includes(to.path)) {
    next()
    return
  }
  if (!token) {
    next('/login')
    return
  }
  try {
    const decodedToken = jwtDecode(token)
    const currentTime = Date.now() / 1000
    if (decodedToken.exp < currentTime) {
      localStorage.removeItem('jwtToken')
      next('/login')
      return
    }
    next()
  } catch (error) {
    console.error('Token解析失败:', error)
    localStorage.removeItem('jwtToken')
    next('/login')
  }
})

export default router
