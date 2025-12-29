<script setup>
import { ref, computed, onMounted, onUnmounted } from 'vue'
import { useScrollStore } from '@/stores/modules/scroll'
import { RouterLink, useRoute } from 'vue-router'
const route = useRoute()
const type = route.params.type
import PaginationComponent from '@/components/common/PaginationComponent.vue'
import CardImgComponent from '@/components/common/CardImgComponent.vue'
const scrollStore = useScrollStore()
const allItems = ref([
  {
    id: 1,
    title: 'Tool',
    content: 'Welcome, it’s great to have you here. We know that first',
    tag: ['Life', 'Music', 'Art'],
    href: 'https://www.baidu.com',
  },
  {
    id: 2,
    title: 'Tool',
    content: 'Welcome, it’s great to have you here. We know that first',
    tag: ['Life', 'Music', 'Art'],
    href: 'https://www.baidu.com',
  },
  {
    id: 3,
    title: 'Tool',
    content: 'Welcome, it’s great to have you here. We know that first',
    tag: ['Life', 'Music', 'Art'],
    href: 'https://www.baidu.com',
  },
  {
    id: 4,
    title: 'Tool',
    content: 'Welcome, it’s great to have you here. We know that first',
    tag: ['Life', 'Music', 'Art'],
    href: 'https://www.baidu.com',
  },
  {
    id: 5,
    title: 'Tool',
    content: 'Welcome, it’s great to have you here. We know that first',
    tag: ['Life', 'Music', 'Art'],
    href: 'https://www.baidu.com',
  },
  {
    id: 6,
    title: 'Tool',
    content: 'Welcome, it’s great to have you here. We know that first',
    tag: ['Life', 'Music', 'Art'],
    href: 'https://www.baidu.com',
  },
  {
    id: 7,
    title: 'Tool',
    content: 'Welcome, it’s great to have you here. We know that first',
    tag: ['Life', 'Music', 'Art'],
    href: 'https://www.baidu.com',
  },
  {
    id: 8,
    title: 'Tool',
    content: 'Welcome, it’s great to have you here. We know that first',
    tag: ['Life', 'Music', 'Art'],
    href: 'https://www.baidu.com',
  },
])
const currentPage = ref(1) // 当前页码
const itemsPerPage = ref(4) // 每页显示的条目数

// 计算总页数
const totalPages = computed(() => {
  return Math.ceil(allItems.value.length / itemsPerPage.value)
})

// 计算当前页显示的条目
const items = computed(() => {
  const startIndex = (currentPage.value - 1) * itemsPerPage.value
  const endIndex = startIndex + itemsPerPage.value
  return allItems.value.slice(startIndex, endIndex)
})
// 处理页码变化事件
const handlePageChange = (page) => {
  currentPage.value = page
  // 可以在这里添加滚动到页面顶部的逻辑
  window.scrollTo({ top: 0, behavior: 'smooth' })
}
onMounted(() => {
  scrollStore.enableScrollListener()
})
onUnmounted(() => {
  scrollStore.disableScrollListener()
})
</script>
<template>
  <div
    class="mt-22 md:mt-45 flex flex-col items-center w-full md:w-7/10"
    :class="scrollStore.isScrolled ? 'md:mt-45' : ''"
  >
    <div class="text-primary font-bold text-2xl">Article / Bug</div>
    <div
      class="flex flex-col md:flex-row md:flex-wrap md:justify-between items-center w-full mt-2 md:mt-10"
    >
      <div
        v-for="(item, index) in items"
        :key="index"
        class="w-full md:w-1/4 p-2"
      >
        <a :href="item.href" target="_blank">
          <CardImgComponent :item="item" :index="index" :footer="false" />
        </a>
      </div>
    </div>
    <div class="mt-2 md:mt-10">
      <PaginationComponent
        :current-page="currentPage"
        :total-pages="totalPages"
        :page-range="5"
        @page-change="handlePageChange"
      />
    </div>
  </div>
</template>
<style lang="less" scoped></style>
