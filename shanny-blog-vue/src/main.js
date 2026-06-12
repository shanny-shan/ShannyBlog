import { createApp } from 'vue'
import pinia from './stores'
import App from './App.vue'
import router from './router'

import { FontAwesomeIcon } from './utils/icons.js'
import './styles/main.css'
import VueCookies from 'vue3-cookies'

import VMdPreview from '@kangc/v-md-editor/lib/preview'
import '@kangc/v-md-editor/lib/style/preview.css'
import githubTheme from '@kangc/v-md-editor/lib/theme/github.js'
import '@kangc/v-md-editor/lib/theme/style/github.css'
import hljs from 'highlight.js'

import createCopyCodePlugin from '@kangc/v-md-editor/lib/plugins/copy-code/index'
import '@kangc/v-md-editor/lib/plugins/copy-code/copy-code.css'
import createTipPlugin from '@kangc/v-md-editor/lib/plugins/tip/index'
import '@kangc/v-md-editor/lib/plugins/tip/tip.css'
import createEmojiPlugin from '@kangc/v-md-editor/lib/plugins/emoji/index'
import '@kangc/v-md-editor/lib/plugins/emoji/emoji.css'
import createMermaidPlugin from '@kangc/v-md-editor/lib/plugins/mermaid/cdn'
import '@kangc/v-md-editor/lib/plugins/mermaid/mermaid.css'
import createTodoListPlugin from '@kangc/v-md-editor/lib/plugins/todo-list/index'
import '@kangc/v-md-editor/lib/plugins/todo-list/todo-list.css'
import createAlignPlugin from '@kangc/v-md-editor/lib/plugins/align'

VMdPreview.use(githubTheme, { Hljs: hljs })

VMdPreview.use(createCopyCodePlugin())
VMdPreview.use(createTipPlugin())
VMdPreview.use(createEmojiPlugin())
VMdPreview.use(createMermaidPlugin())
VMdPreview.use(createTodoListPlugin())
VMdPreview.use(createAlignPlugin())

import Toast from 'vue-toastification'
import 'vue-toastification/dist/index.css'
const toastOptions = {
  position: 'top-right',
  timeout: 1500,
  pauseOnFocusLoss: true,
  pauseOnHover: true,
  draggable: true,
  draggablePercent: 0.6,
  showCloseButtonOnHover: true,
  icon: true,
  rtl: false,
}

const app = createApp(App)
app.component('font-awesome-icon', FontAwesomeIcon)
app.use(pinia)
app.use(router)
app.use(VueCookies)
app.use(Toast, toastOptions)
app.use(VMdPreview)
app.mount('#app')
