import { defineStore } from 'pinia'
import { ref } from 'vue'
import {
  loginUser,
  registerUser,
  getInfo,
  getUsers,
  updateUserInfo,
  deleteUserByUuid,
} from '@/apis/user'
import router from '@/router'
import { useSiteStore } from '@/stores'

export const useAccountStore = defineStore('account', () => {
  const siteStore = useSiteStore()

  const users = ref([])
  const userInfo = ref({})

  /**
   * account msg
   */
  const loginForm = ref({
    userId: '',
    password: '',
  })
  const registerForm = ref({
    userId: '',
    mobile: '',
    password: '',
    confirmPassword: '',
  })
  const userForm = ref({
    userDetails: {},
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

  const getUserInfo = async () => {
    const res = await getInfo()
    if (res) {
      userInfo.value = res.data.data
    }
  }

  /**
   * getUsers
   */

  const getAllUsers = async () => {
    siteStore.loading = true
    const res = await getUsers()
    if (res) {
      users.value = res.data.data || []
      siteStore.loading = false
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

  const editUserInfo = async (info) => {
    return await updateUserInfo(info)
  }
  const deleteUser = async (uuid) => {
    return await deleteUserByUuid(uuid)
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
    userInfo,
    getUserInfo,

    // getUsers
    users,
    getAllUsers,

    // logout
    logout,

    editUserInfo,
    deleteUser,
  }
})
