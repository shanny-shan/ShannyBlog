<script setup>
import { ref, computed, onMounted, onUnmounted } from 'vue'
import { useScrollStore } from '@/stores/modules/scroll'
import { useArticleStore } from '@/stores/modules/article'
import { RouterLink } from 'vue-router'
import CardImgComponent from '@/components/common/CardImgComponent.vue'
import PaginationComponent from '@/components/common/PaginationComponent.vue'
const scrollStore = useScrollStore()
const articleStore = useArticleStore()
const noteList = ref([])
const currentPage = ref(1)
const itemsPerPage = ref(4)

const getNoteList = async () => {
  const res = await articleStore.getArticleByTypes('ARTICLE_NOTE')
  console.log(res.data.data)
  if(res.data.code.toLowerCase() === 'success'){
    noteList.value = res.data.data
  }
}

// pagination-------------------------------------------------------------------------------------
// 计算总页数
const totalPages = computed(() => {
  return Math.ceil(noteList.value.length / itemsPerPage.value)
})

// 计算当前页显示的条目
const items = computed(() => {
  const startIndex = (currentPage.value - 1) * itemsPerPage.value
  const endIndex = startIndex + itemsPerPage.value
  return noteList.value.slice(startIndex, endIndex)
})
// 处理页码变化事件
const handlePageChange = (page) => {
  currentPage.value = page
  // 可以在这里添加滚动到页面顶部的逻辑
  window.scrollTo({ top: 0, behavior: 'smooth' })
}

onMounted(() => {
  scrollStore.enableScrollListener()
  getNoteList()
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
    <div class="text-primary font-bold text-2xl">Article / Note</div>
    <div
      class="flex flex-col md:flex-row md:flex-wrap md:justify-between items-center w-full mt-2 md:mt-10"
    >
      <div
        v-for="(item, index) in noteList"
        :key="index"
        class="w-full md:w-1/4 p-2"
      >
        <RouterLink :to="`/article/note/${item.id}`">
          <CardImgComponent :item="item" :index="index" />
        </RouterLink>
      </div>
    </div>
    <div class="mt-2 md:mt-10" v-if="noteList.length > 1">
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
