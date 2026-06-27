import axios from 'axios'
import router from '@/router'
import { useToast } from 'vue-toastification'

const baseURL = import.meta.env.VITE_BASE_API
const token = localStorage.getItem('jwtToken')
const toast = useToast()

const instance = axios.create({
  baseURL,
  timeout: 10000,
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
  (err) => Promise.reject(err),
)

// 响应拦截器
instance.interceptors.response.use(
  (res) => {
    //返回的数据
    return res
  },
  (err) => {
    if (err.code === 'ECONNABORTED' || err.message.includes('timeout')) {
      toast.error('请求超时，请检查网络或稍后重试')
    } else if (!err.response) {
      toast.error('网络连接失败，无法访问服务器')
    } else {
      const status = err.response.status
      switch (status) {
        case 401:
          localStorage.removeItem('jwtToken')
          toast.warning('登录已过期，请重新登录')
          router.push('/')
          return Promise.resolve()
        case 403:
          toast.error('无权限访问')
          break
        case 404:
          toast.error('请求接口不存在')
          break
        case 500:
          toast.error('服务器内部错误')
          break
        default:
          toast.error(`请求失败：${status}`)
      }
    }
    return Promise.reject(err)
  },
)
export default instance
