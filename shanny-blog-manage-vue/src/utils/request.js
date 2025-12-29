import axios from 'axios'
import router from '@/router'

const baseURL = '/api'
const token = localStorage.getItem('jwtToken')

const instance = axios.create({
  baseURL,
  timeout: 5000,
  headers: {
    Authorization: token ? token : '',
  },
})

// 请求拦截器
instance.interceptors.request.use(
  (config) => {
    // 提交的数据
    return config
  },
  (err) => Promise.reject(err)
)

// 响应拦截器
instance.interceptors.response.use(
  (res) => {
    //返回的数据
    return res
  },
  (err) => {
    if (err.response && err.response.status === 401) {
      localStorage.removeItem('jwtToken')
      router.push('/')
      return Promise.resolve()
    }
    return Promise.reject(err)
  }
)
export default instance
