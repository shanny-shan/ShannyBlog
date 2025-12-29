<script setup>
import { onMounted, onUnmounted } from 'vue'
import LayoutHeader from './components/LayoutHeader.vue'
import LayoutFooter from './components/LayoutFooter.vue'
import { useThemeStore } from '@/stores/modules/theme'
import { useScrollStore } from '@/stores/modules/scroll'
const scrollStore = useScrollStore()
const themeStore = useThemeStore()
onMounted(() => {
  scrollStore.enableScrollListener()
})
onUnmounted(() => {
  scrollStore.disableScrollListener()
})
</script>
<template>
  <div class="layout flex w-full" :data-theme="themeStore.currentTheme">
    <div class="flex flex-col items-center w-full">
      <div class="flex justify-center w-full">
        <LayoutHeader />
      </div>
      <div class="flex justify-center w-full">
        <RouterView :key="$route.fullPath" />
      </div>
      <div class="flex justify-center w-full">
        <LayoutFooter />
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped></style>
