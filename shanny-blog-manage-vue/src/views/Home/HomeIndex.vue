<script setup>
import HomeMobile from './HomeMobile.vue'
import HomePC from './HomePC.vue'
import { useSiteStore } from '@/stores'
const siteStore = useSiteStore()
</script>
<template>
  <HomeMobile v-if="siteStore.isMobile" />
  <HomePC v-else />
</template>
<style lang="scss" scoped></style>
