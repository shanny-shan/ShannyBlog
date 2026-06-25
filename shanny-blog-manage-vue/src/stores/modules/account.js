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
import { useSiteStore, useAdminStore } from '@/stores'
import { swal } from '@/utils/sweetalert'
import { useToast } from 'vue-toastification'

export const useAccountStore = defineStore('account', () => {
  const toast = useToast()
  const siteStore = useSiteStore()
  const adminStore = useAdminStore()

  const users = ref([])
  const userInfo = ref({})
  const accountType = ref('login')

  const sexOptions = ref({
    UNKNOWN: '未知',
    MAN: '男',
    FEMALE: '女',
  })
  const statusOptions = ref({
    ACTIVE: '启用',
    LOCKED: '锁定',
    DELETED: '删除',
  })
  const DEFAULT_TEXT = ref('请选择日期')

  const selectedDate = ref(DEFAULT_TEXT.value)
  const calendarRef = ref(null)
  const dropdownOpen = ref(false)

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

  const login = async () => {
    const { userId, password } = loginForm.value
    siteStore.loading = true

    const res = await loginUser(userId, password)
    if (res.data.code.toLowerCase() == 'success') {
      toast.success(`Wecome ${userId}!`)
      localStorage.setItem('jwtToken', res.data.data.token)
      setTimeout(() => {
        window.location.href = '/manage/'
      }, 2000)
    } else {
      toast.error(res.data.msg)
      siteStore.loading = false
    }
  }
  const register = async () => {
    const { userId, mobile, password, confirmPassword } = registerForm.value

    if (password !== confirmPassword) {
      toast.error('两次输入的密码不一致')
      return
    }

    siteStore.loading = true

    const res = await registerUser(userId, mobile, password)
    if (res.data.code.toLowerCase() == 'success') {
      toast.success(res.data.msg)
      loginForm.value.userId = userId
      loginForm.value.password = password
      login()
    } else {
      toast.error(res.data.msg)
      siteStore.loading = false
    }
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
    swal('', '确定退出登陆吗？', '', 'question', true, true).then((result) => {
      if (result.isConfirmed) {
        userInfo.value.value = {}
        localStorage.removeItem('jwtToken')
        router.push('/login')
      }
    })
  }

  /**
   * info
   */

  const openEditInfo = (item) => {
    adminStore.openDialog('info')
    userForm.value = {
      ...item,
      userDetails: { ...item.userDetails },
    }
    adminStore.isEdit = true
  }
  const deleteUser = (item) => {
    swal(
      '',
      '',
      `确定删除名为<span class="text-primary font-bold">${item.userId}</span>的账户吗？`,
      'question',
      true,
      true,
    ).then(async (result) => {
      if (result.isConfirmed) {
        const res = await deleteUserByUuid(item.uuid)
        if (res.data.code.toLowerCase() === 'success') {
          toast.success(`${res.data.msg}`)
          await getAllUsers()
        } else {
          toast.error(`${res.data.msg}`)
        }
      }
    })
  }

  const handleDateChange = (e) => {
    const date = e.target.value
    selectedDate.value = date
    userForm.value.userDetails.birthday = date
    dropdownOpen.value = false
  }

  const setDate = () => {
    const birthday = userForm.value.userDetails.birthday
    if (birthday) {
      selectedDate.value = birthday
      if (calendarRef.value) {
        calendarRef.value.value = birthday
      }
    } else {
      selectedDate.value = DEFAULT_TEXT.value
    }
  }

  const submitInfo = async () => {
    siteStore.loading = true

    if (adminStore.isEdit) {
      const res = await updateUserInfo(userForm.value)
      if (res.data.code.toLowerCase() === 'success') {
        toast.success(`${res.data.msg}`)
        adminStore.closeDialog('info')
        await getAllUsers()
      } else {
        toast.error(`${res.data.msg}`)
      }
    }

    siteStore.loading = false
  }

  return {
    accountType,
    sexOptions,
    statusOptions,
    DEFAULT_TEXT,
    selectedDate,
    calendarRef,
    dropdownOpen,
    handleDateChange,
    submitInfo,
    setDate,

    // account msg
    loginForm,
    registerForm,
    userForm,

    login,
    register,

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

    openEditInfo,
    deleteUser,
  }
})
