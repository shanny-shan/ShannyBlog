<script setup>
import { ref, onMounted } from 'vue'
import { useAccountStore } from '@/stores/modules/account'
import { useToast } from 'vue-toastification'
import { useSiteStore } from '@/stores/modules/site'
const toast = useToast()
const accountType = ref('login')
const accountStore = useAccountStore()
const siteStore = useSiteStore()
const login = async (userId, password) => {
  const res = await accountStore.getLogin(userId, password)
  siteStore.loading = true
  if (res.data.code.toLowerCase() == 'success') {
    toast.success(`Wecome ${userId}!`)
    localStorage.setItem('jwtToken', res.data.data.token)
    setTimeout(() => {
      window.location.href = '/'
    }, 2000)
  } else {
    toast.error(res.data.msg)
    siteStore.loading = false
  }
}
const register = async (userId, mobile, password) => {
  const res = await accountStore.getRegister(userId, mobile, password)
  siteStore.loading = true
  if (res.data.code.toLowerCase() == 'success') {
    toast.success(res.data.msg)
    login(userId, password)
  } else {
    toast.error(res.data.msg)
    siteStore.loading = false
  }
}

onMounted(async () => {
  if (accountStore.isLoggedIn()) {
    await accountStore.getUserInfo()
  } else {
    accountStore.userInfo = {}
  }
})
</script>
<template>
  <div class="flex justify-center items-center w-full h-screen">
    <div class="card card-border bg-base-200 w-96">
      <div class="card-body">
        <div class="flex flex-row w-full justify-center items-center gap-2">
          <button
            class="btn"
            :class="accountType == 'login' ? 'btn-primary' : 'btn-ghost'"
            @click="accountType = 'login'"
          >
            Login
          </button>
          <button
            class="btn"
            :class="accountType == 'register' ? 'btn-primary' : 'btn-ghost'"
            @click="accountType = 'register'"
          >
            Register
          </button>
        </div>
        <fieldset
          class="fieldset bg-base-100 border-primary shadow-sm rounded-box w-full max-w-full border p-4 h-full max-h-full flex flex-col"
        >
          <legend class="fieldset-legend">
            {{ accountType == 'login' ? 'Login' : 'Register' }}
          </legend>
          <div v-if="accountType == 'login'">
            <label class="label w-full">UserId</label>
            <input
              type="text"
              class="input input-primary bg-base-200 w-full"
              placeholder="Please input userId"
              v-model="accountStore.loginForm.userId"
            />

            <label class="label w-full mt-5">Password</label>
            <input
              type="password"
              class="input input-primary bg-base-200 w-full"
              placeholder="Please input password"
              v-model="accountStore.loginForm.password"
            />

            <button
              class="btn btn-primary w-full mt-5"
              @click="
                login(
                  accountStore.loginForm.userId,
                  accountStore.loginForm.password
                )
              "
            >
              Login
            </button>
          </div>
          <div v-else>
            <label class="label w-full">UserId</label>
            <input
              type="text"
              class="input input-primary bg-base-200 w-full"
              placeholder="Please input userId"
              v-model="accountStore.registerForm.userId"
            />

            <label class="label w-full mt-5">Mobile</label>
            <input
              type="text"
              class="input input-primary bg-base-200 w-full"
              placeholder="Please input mobile"
              v-model="accountStore.registerForm.mobile"
            />

            <label class="label w-full mt-5">Password</label>
            <input
              type="password"
              class="input input-primary bg-base-200 w-full"
              placeholder="Please input password"
              v-model="accountStore.registerForm.password"
            />

            <label class="label w-full mt-5">Confirm Password</label>
            <input
              type="password"
              class="input input-primary bg-base-200 w-full"
              placeholder="Please input confirm password"
              v-model="accountStore.registerForm.confirmPassword"
            />

            <button
              class="btn btn-primary w-full mt-5"
              @click="
                register(
                  accountStore.registerForm.userId,
                  accountStore.registerForm.mobile,
                  accountStore.registerForm.password
                )
              "
            >
              Register
            </button>
          </div>
        </fieldset>
      </div>
    </div>
  </div>
</template>
<style lang="scss" scoped></style>
