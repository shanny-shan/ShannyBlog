<script setup>
import { useSiteStore } from './stores/modules/site'
const siteStore = useSiteStore()
</script>
<template>
  <div
    v-if="siteStore.loading"
    class="fixed inset-0 flex w-full h-100vh items-center justify-center bg-black/40 z-[9999]"
  >
    <span class="loading loading-bars loading-xl"></span>
  </div>
  <RouterView />
</template>

<style scoped></style>
