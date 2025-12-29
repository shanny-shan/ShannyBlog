<script setup>
import { ref, computed, onMounted, onUnmounted } from 'vue'
import { useScrollStore } from '@/stores/modules/scroll'
import { RouterLink, useRoute } from 'vue-router'
const route = useRoute()
const type = route.params.type
import CardImgComponent from '@/components/common/CardImgComponent.vue'
import PaginationComponent from '@/components/common/PaginationComponent.vue'
const scrollStore = useScrollStore()
const allItems = ref([
  {
    id: 1,
    title: 'The spectacle before us was indeed sublime',
    content:
      'Welcome, it’s great to have you here. We know that first impressions are important, so we’ve populated your new site…',
    date: ' September 26, 2019',
    tag: ['Life', 'Music', 'Art'],
    readCount: 12,
    rate: 4,
    author: 'Shanny',
  },
  {
    id: 2,
    title: 'Far far away, behind the word mountains',
    content:
      'Far far away, behind the word mountains, far from the countries Vokalia and Consonantia Separated…',
    date: 'August 15, 2019',
    tag: ['Music', 'Art'],
    readCount: 160,
    rate: 3,
    author: 'Shanny',
  },
  {
    id: 3,
    title: 'The meaning of health has evolved over time',
    content:
      'In keeping with the biomedical perspective, early definitions of health focused on the theme of the body’s ability to function;…',
    date: 'July 26, 2019',
    tag: ['Art'],
    readCount: 3,
    rate: 2,
    author: 'Shanny',
  },
  {
    id: 4,
    title: 'Musical improvisation is the spontaneous music',
    content:
      'It was one of the worst storms to hit London since God knows when. The thunder rolled, lightning flashed and…',
    date: 'July 26, 2019',
    tag: ['Life', 'Music'],
    readCount: 150,
    rate: 5,
    author: 'Shanny',
  },
  {
    id: 5,
    title: 'Customizing your brand and design settings',
    content:
      'The Ghost editor has everything you need to fully optimise your content. This is where you can add tags and…',
    date: 'July 26, 2019',
    tag: ['Music'],
    readCount: 10,
    rate: 3,
    author: 'Shanny',
  },
  {
    id: 6,
    title: 'The meaning of health has evolved over time',
    content:
      'In keeping with the biomedical perspective, early definitions of health focused on the theme of the body’s ability to function;…',
    date: 'July 26, 2019',
    tag: ['Art'],
    readCount: 3,
    rate: 2,
    author: 'Shanny',
  },
  {
    id: 7,
    title: 'Musical improvisation is the spontaneous music',
    content:
      'It was one of the worst storms to hit London since God knows when. The thunder rolled, lightning flashed and…',
    date: 'July 26, 2019',
    tag: ['Life', 'Music'],
    readCount: 150,
    rate: 5,
    author: 'Shanny',
  },
  {
    id: 8,
    title: 'Customizing your brand and design settings',
    content:
      'The Ghost editor has everything you need to fully optimise your content. This is where you can add tags and…',
    date: 'July 26, 2019',
    tag: ['Music'],
    readCount: 10,
    rate: 3,
    author: 'Shanny',
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
    <div class="text-primary font-bold text-2xl">Article / Node</div>
    <div
      class="flex flex-col md:flex-row md:flex-wrap md:justify-between items-center w-full mt-2 md:mt-10"
    >
      <div
        v-for="(item, index) in items"
        :key="index"
        class="w-full md:w-1/4 p-2"
      >
        <RouterLink :to="`/article/note/${item.id}`">
          <CardImgComponent :item="item" :index="index" />
        </RouterLink>
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
