<script setup>
import { ref, onMounted, onUnmounted } from 'vue'
import { useScrollStore, useSiteStore } from '@/stores'
import HomeAbout from '@/views/Home/compoments/HomeAbout.vue'
import HomeTag from '@/views/Home/compoments/HomeTag.vue'
import HomeRecent from '@/views/Home/compoments/HomeRecent.vue'
import HomeArticle from '@/views/Home/compoments/HomeArticle.vue'
const scrollStore = useScrollStore()
const siteStore = useSiteStore()

const loadCount = ref(0)
const handleLoadComplete = () => {
  loadCount.value++
  if (loadCount.value === 3) {
    siteStore.loading = false
  }
}

onMounted(() => {
  siteStore.loading = true
  scrollStore.enableScrollListener()
})
onUnmounted(() => {
  scrollStore.disableScrollListener()
})
</script>
<template>
  <div
    class="mt-22 md:mt-45 flex flex-col w-full md:w-7/10"
    :class="scrollStore.isScrolled ? 'md:mt-45' : ''"
  >
    <div class="flex flex-col md:flex-row justify-between w-full">
      <div
        class="w-full md:w-1/3 xl:w-1/4 flex flex-col md:items-start pl-2 pr-2 md:pl-0 md:pr-5"
      >
        <HomeAbout @load-complete="handleLoadComplete" />
        <HomeTag @load-complete="handleLoadComplete" />
        <HomeRecent @load-complete="handleLoadComplete" />
      </div>
      <div
        class="w-full md:w-2/3 xl:w-3/4 flex flex-col pl-2 pr-2 md:pr-0 md:pl-5"
      >
        <HomeArticle @load-complete="handleLoadComplete" />
      </div>
    </div>
  </div>
</template>
<style lang="scss" scoped></style>
