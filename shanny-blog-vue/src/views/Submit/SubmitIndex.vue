<script setup>
import { onMounted, onUnmounted } from 'vue'
import { useScrollStore } from '@/stores/modules/scroll'
import { useRoute } from 'vue-router'
import DetailMessage from '@/views/_components/common/DetailMessage.vue'
const route = useRoute()
const category = route.params.category
const id = route.params.id
const scrollStore = useScrollStore()
onMounted(() => {
  scrollStore.enableScrollListener()
})
onUnmounted(() => {
  scrollStore.disableScrollListener()
})
</script>
<template>
  <div
    class="mt-18 md:mt-35 flex flex-col items-center w-full md:w-7/10 p-2 md:p-0"
    :class="scrollStore.isScrolled ? 'md:mt-35' : ''"
  >
    <div class="text-primary font-bold text-2xl">
      Submit / {{ category }} {{ id.length == 0 ? '' : `/ ${id}` }}
    </div>
    <div>note message form</div>
  </div>
</template>
<style lang="less" scoped></style>
