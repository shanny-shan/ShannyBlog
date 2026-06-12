<script setup>
import { ref, onMounted } from 'vue'
import { useAccountStore } from '@/stores/modules/account'
import { useToast } from 'vue-toastification'
import { useSiteStore } from '@/stores/modules/site'
const toast = useToast()
const accountType = ref('login')
const accountStore = useAccountStore()
const siteStore = useSiteStore()
const login = async () => {
  const { userId, password } = accountStore.loginForm
  siteStore.loading = true

  const res = await accountStore.getLogin(userId, password)
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
  const { userId, mobile, password, confirmPassword } =
    accountStore.registerForm

  if (password !== confirmPassword) {
    toast.error('两次输入的密码不一致')
    return
  }

  siteStore.loading = true

  const res = await accountStore.getRegister(userId, mobile, password)
  if (res.data.code.toLowerCase() == 'success') {
    toast.success(res.data.msg)
    accountStore.loginForm.userId = userId
    accountStore.loginForm.password = password
    login()
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
const goWebSite = () => {
  window.open('https://www.shanny.work', '_blank')
}
</script>
<template>
  <div class="flex justify-center items-center w-full h-screen">
    <div class="card card-border bg-base-200 w-96">
      <button
        class="btn btn-ghost tooltip absolute top-2 left-2"
        data-tip="GO WebSite"
        @click="goWebSite()"
      >
        <font-awesome-icon icon="fa-regular fa-circle-left" />
      </button>
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
          class="fieldset bg-base-100 border-primary shadow-sm rounded-box w-full max-w-full border p-4 h-full max-h-full"
        >
          <legend class="fieldset-legend">
            {{ accountType == 'login' ? 'Login' : 'Register' }}
          </legend>

          <form @submit.prevent="login" v-if="accountType == 'login'">
            <label class="label w-full">UserId</label>
            <input
              type="text"
              class="input validator input-primary bg-base-200 w-full"
              required
              placeholder="测试账号：test"
              v-model="accountStore.loginForm.userId"
            />

            <label class="label w-full mt-5">Password</label>
            <input
              type="password"
              class="input validator input-primary bg-base-200 w-full"
              required
              placeholder="测试密码：Test1234"
              v-model="accountStore.loginForm.password"
            />

            <div class="flex flex-col gap-2 mt-5">
              <button type="submit" class="btn btn-primary w-full">
                Login
              </button>
              <!-- <div class="flex justify-end">
                <button
                  @click="resetPassword()"
                  type="button"
                  class="btn btn-link btn-xs"
                >
                  忘记密码？
                </button>
              </div> -->
            </div>
          </form>
          <form @submit.prevent="register" v-else>
            <fieldset class="fieldset">
              <label class="label w-full">UserId</label>
              <input
                type="text"
                class="input validator input-primary bg-base-200 w-full"
                required
                pattern="[A-Za-z][A-Za-z0-9\-]*"
                placeholder="Please input userId"
                minlength="3"
                maxlength="30"
                title="长度必须为 3-30 个字符，仅包含字母、数字或短横线"
                v-model="accountStore.registerForm.userId"
              />
              <!-- <p class="validator-hint hidden">
                长度必须为 3-30 个字符，仅包含字母、数字或短横线
              </p> -->
            </fieldset>

            <fieldset class="fieldset">
              <label class="label w-full mt-5">Mobile</label>
              <input
                type="tel"
                class="input validator tabular-nums input-primary bg-base-200 w-full"
                required
                placeholder="Please input mobile"
                pattern="^1[3-9]\d{9}$"
                minlength="11"
                maxlength="11"
                title="请输入11位手机号"
                v-model="accountStore.registerForm.mobile"
              />
              <!-- <p class="validator-hint hidden">请输入11位手机号</p> -->
            </fieldset>

            <fieldset class="fieldset">
              <label class="label w-full mt-5">Password</label>
              <input
                type="password"
                class="input validator input-primary bg-base-200 w-full"
                required
                minlength="8"
                pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}"
                placeholder="Please input password"
                title="长度必须大于 8 个字符，且同时包含数字、小写字母、大写字母"
                v-model="accountStore.registerForm.password"
              />
              <!-- <p class="validator-hint hidden">
                长度必须大于 8 个字符，且同时包含数字、小写字母、大写字母
              </p> -->
            </fieldset>

            <fieldset class="fieldset">
              <label class="label w-full mt-5">Confirm Password</label>
              <input
                type="password"
                class="input validator input-primary bg-base-200 w-full"
                required
                minlength="8"
                pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}"
                placeholder="Please input password"
                title="长度必须大于 8 个字符，且同时包含数字、小写字母、大写字母"
                v-model="accountStore.registerForm.confirmPassword"
              />
              <!-- <p class="validator-hint hidden">
                长度必须大于 8 个字符，且同时包含数字、小写字母、大写字母
              </p> -->
            </fieldset>

            <button
              class="btn btn-primary w-full mt-5"
              type="submit"
              :disabled="siteStore.loading"
            >
              Register
            </button>
          </form>
        </fieldset>
      </div>
    </div>
  </div>
</template>
<style lang="scss" scoped></style>
