<script setup>
import { useArticleStore } from '@/stores/modules/article'
const articleStore = useArticleStore()
const handleUploadImage = (event, insertImage, files) => {
  insertImage({
    url: '',
    desc: 'desc',
    width: 'auto',
    height: 'auto',
  })
}
// mermaid语法 https://mermaid.js.org/syntax/flowchart.html
const toolbar = {
  code: {
    title: '代码块和流程图',
    icon: 'v-md-icon-code',
    menus: [
      {
        name: 'code',
        text: '代码块',
        action(editor) {
          editor.insert(function (selected) {
            const prefix = '\`\`\`language\n'
            const suffix = '\n\`\`\`\n'
            const placeholder = 'code'
            const content = selected || placeholder

            return {
              text: `${prefix}${content}${suffix}`,
              selected: content,
            }
          })
        },
      },
      {
        name: 'mermaid',
        text: '流程图',
        action(editor) {
          editor.insert(function (selected) {
            const prefix = '\`\`\`mermaid\n'
            const suffix = '\n\`\`\`\n'
            const placeholder = `graph LR
A --- B
B-->C[fa:fa-ban forbidden]
B-->D(fa:fa-spinner)`
            const content = selected || placeholder

            return {
              text: `${prefix}${content}${suffix}`,
              selected: content,
            }
          })
        },
      },
    ],
  },
  align: {
    title: '对齐方式',
    icon: 'v-md-icon-toc',
    menus: [
      {
        name: 'left',
        text: '左对齐',
        action(editor) {
          editor.insert(function (selected) {
            const prefix = '::: align-left\n'
            const suffix = '\n:::\n'
            const placeholder = 'left'
            const content = selected || placeholder
            return {
              text: `${prefix}${content}${suffix}`,
              selected: content,
            }
          })
        },
      },
      {
        name: 'center',
        text: '居中',
        action(editor) {
          editor.insert(function (selected) {
            const prefix = '::: align-center\n'
            const suffix = '\n:::\n'
            const placeholder = 'center'
            const content = selected || placeholder
            return {
              text: `${prefix}${content}${suffix}`,
              selected: content,
            }
          })
        },
      },
      {
        name: 'right',
        text: '右对齐',
        action(editor) {
          editor.insert(function (selected) {
            const prefix = '::: align-right\n'
            const suffix = '\n:::\n'
            const placeholder = 'right'
            const content = selected || placeholder
            return {
              text: `${prefix}${content}${suffix}`,
              selected: content,
            }
          })
        },
      },
    ],
  },
}
</script>
<template>
  <!-- @upload-image="handleUploadImage" -->
  <v-md-editor
    v-model="articleStore.articleForm.content"
    :disabled-menus="[]"
    height="100%"
    left-toolbar="undo redo clear | align h bold italic strikethrough quote | ul ol table hr todo-list | link code tip emoji | save"
    :toolbar="toolbar"
  >
  </v-md-editor>
</template>
