import { useCookies } from 'vue3-cookies'
const { cookies } = useCookies()

const setCookie = (name, value, time) => {
  cookies.set(name, value, time)
}
const getCookie = (name) => {
  return cookies.get(name)
}
const removeCookie = (name) => {
  cookies.remove(name)
}
export { setCookie, getCookie, removeCookie }
