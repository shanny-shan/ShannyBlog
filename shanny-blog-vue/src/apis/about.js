import request from '@/utils/request'

const getAbout = () => {
  return request({
    url: '/about/get',
    method: 'Get',
  })
}

const insertAbout = (about) => {
  return request({
    url: '/about/add',
    method: 'POST',
    data: about,
  })
}

export { getAbout, insertAbout }
