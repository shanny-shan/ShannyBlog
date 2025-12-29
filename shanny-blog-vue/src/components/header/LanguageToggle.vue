<script setup>
import { onMounted } from 'vue'
import { useLanguageStore } from '@/stores/modules/language'
const languageStore = useLanguageStore()
onMounted(() => {
  languageStore.initLanguage()
})
</script>
<template>
  <label class="swap swap-rotate">
    <input
      type="checkbox"
      class="theme-controller"
      value="synthwave"
      v-model="languageStore.isEnglish"
    />
    <div class="swap-off">
      <font-awesome-icon icon="fa-solid fa-language" />
      <span class="font-bold">中</span>
    </div>
    <div class="swap-on">
      <font-awesome-icon icon="fa-solid fa-language" />
      <span class="font-bold">En</span>
    </div>
  </label>
</template>
<style lang="scss" scoped></style>
