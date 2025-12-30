import request from '@/utils/request'

const getToolAll = () => {
  return request({
    url: '/tool/all',
    method: 'Get',
  })
}

export { getToolAll }
