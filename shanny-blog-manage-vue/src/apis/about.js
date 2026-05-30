import request from '@/utils/request'

const getAbout = () => {
  return request({
    url: '/about/all',
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

const updateAbout = (tool) => {
  return request({
    url: '/about/update',
    method: 'Post',
    data: tool,
  })
}
const deleteAboutById = (id) => {
  return request({
    url: '/about/delete',
    method: 'Post',
    params: {
      id,
    },
  })
}

export { getAbout, insertAbout, updateAbout, deleteAboutById }
