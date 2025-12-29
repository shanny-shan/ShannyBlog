import request from '@/utils/request'
const registerUser = (userId, mobile, password) => {
  return request({
    url: '/account/register',
    method: 'POST',
    data: {
      userId,
      mobile,
      password,
    },
  })
}
const loginUser = (userId, password) => {
  return request({
    url: '/account/login',
    method: 'POST',
    data: {
      userId,
      password,
    },
  })
}
const getInfo = () => {
  return request({
    url: '/account/userinfo',
    method: 'Get',
  })
}

const getUsers = () => {
  return request({
    url: '/account/users',
    method: 'Get',
  })
}

export { registerUser, loginUser, getInfo, getUsers }
