import request from '@/utils/request'
const getWebInfo = () => {
  return request({
    url: '/project/info',
    method: 'Get',
  })
}

export { getWebInfo }
