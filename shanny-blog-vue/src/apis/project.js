import request from '@/utils/request'
const getProjectInfo = () => {
  return request({
    url: '/project/info',
    method: 'Get',
  })
}

export { getProjectInfo }
