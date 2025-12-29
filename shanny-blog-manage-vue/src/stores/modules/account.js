import { defineStore } from 'pinia'
import { ref } from 'vue'
import { loginUser, registerUser, getInfo, getUsers } from '@/apis/user'
import router from '@/router'

export const useAccountStore = defineStore('account', () => {
  /**
   * account msg
   */
  const loginForm = ref({
    userId: 'shanny',
    password: '123456',
  })
  const registerForm = ref({
    userId: 'shanny',
    mobile: '15603214235',
    password: '123456',
    confirmPassword: '123456',
  })
  /**
   * user register
   */

  const getRegister = async (userId, mobile, password) => {
    return await registerUser(userId, mobile, password)
  }

  /**
   * user login
   */
  const getLogin = async (userId, password) => {
    return await loginUser(userId, password)
  }

  /**
   * isLogin
   */

  const isLoggedIn = () => {
    const token = localStorage.getItem('jwtToken')
    return !!token
  }

  /**
   * getUserInfo
   */
  const userInfo = ref({})
  const getUserInfo = async () => {
    const res = await getInfo()
    if (res) {
      userInfo.value = res.data.data
    }
  }

  /**
   * getUsers
   */

  const users = ref([])
  const getAllUsers = async () => {
    const res = await getUsers()
    if (res) {
      users.value = res.data.data
    }
  }

  /**
   * logout
   */
  const logout = () => {
    userInfo.value.value = {}
    localStorage.removeItem('jwtToken')
    router.push('/login')
  }

  return {
    // account msg
    loginForm,
    registerForm,

    // user register
    getRegister,

    // user login
    getLogin,

    // isLogin
    isLoggedIn,

    // getUserInfo
    userInfo,
    getUserInfo,

    // getUsers
    users,
    getAllUsers,

    // logout
    logout,
  }
})
