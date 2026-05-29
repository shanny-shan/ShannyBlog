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
  const userForm = ref({})
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
    return await getUsers()
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
    userForm,

    // user register
    getRegister,

    // user login
    getLogin,

    // isLogin
    isLoggedIn,

    // getUserInfo
    users,
    userInfo,
    getUserInfo,

    // getUsers
    users,
    getAllUsers,

    // logout
    logout,
  }
})
