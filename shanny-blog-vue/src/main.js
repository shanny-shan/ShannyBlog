import { createApp } from 'vue'
import pinia from './stores'
import App from './App.vue'
import router from './router'

import { FontAwesomeIcon } from './config/icons.js'
import './styles/main.css'
import VueCookies from 'vue3-cookies'

import VMdEditor from '@kangc/v-md-editor/lib/codemirror-editor'
import '@kangc/v-md-editor/lib/style/codemirror-editor.css'
import githubTheme from '@kangc/v-md-editor/lib/theme/github.js'
import '@kangc/v-md-editor/lib/theme/style/github.css'
import hljs from 'highlight.js'

import VMdPreview from '@kangc/v-md-editor/lib/preview'
import '@kangc/v-md-editor/lib/style/preview.css'

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
import Codemirror from 'codemirror'
import 'codemirror/mode/markdown/markdown'
import 'codemirror/mode/javascript/javascript'
import 'codemirror/mode/css/css'
import 'codemirror/mode/htmlmixed/htmlmixed'
import 'codemirror/mode/vue/vue'
import 'codemirror/addon/edit/closebrackets'
import 'codemirror/addon/edit/closetag'
import 'codemirror/addon/edit/matchbrackets'
import 'codemirror/addon/display/placeholder'
import 'codemirror/addon/selection/active-line'
import 'codemirror/addon/scroll/simplescrollbars'
import 'codemirror/addon/scroll/simplescrollbars.css'
import 'codemirror/lib/codemirror.css'

VMdEditor.Codemirror = Codemirror
VMdEditor.use(githubTheme, { Hljs: hljs })
VMdEditor.use(createCopyCodePlugin())
VMdEditor.use(createTipPlugin())
VMdEditor.use(createEmojiPlugin())
VMdEditor.use(createMermaidPlugin())
VMdEditor.use(createTodoListPlugin())
VMdEditor.use(createAlignPlugin())

VMdPreview.use(githubTheme, { Hljs: hljs })

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
app.use(VMdEditor)
app.use(VMdPreview)
app.mount('#app')
