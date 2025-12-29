import { createPinia } from 'pinia'
const pinia = createPinia()
export default pinia

export * from './modules/admin'
export * from './modules/theme'
export * from './modules/language'
export * from './modules/account'
export * from './modules/site'
