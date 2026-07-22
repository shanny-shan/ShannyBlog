<script setup>
import { onMounted, onUnmounted } from 'vue'
import { useScrollStore, useArticleStore, usePageStore } from '@/stores'
import { RouterLink } from 'vue-router'
import CardImgComponent from '@/views/_components/common/CardImgComponent.vue'
import PaginationComponent from '@/views/_components/common/PaginationComponent.vue'

const scrollStore = useScrollStore()
const articleStore = useArticleStore()
const pageStore = usePageStore()

const { pageList, totalPages } = pageStore.getPageData(
  () => articleStore.noteList,
)

onMounted(async () => {
  pageStore.handlePageChange(1)
  scrollStore.enableScrollListener()
  await articleStore.getArticleByTypes('ARTICLE_NOTE')
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
      class="flex flex-col md:flex-row md:flex-wrap md:justify-start items-center w-full mt-2 md:mt-10"
    >
      <div v-for="item in pageList" :key="item.id" class="w-full md:w-1/4 p-2">
        <RouterLink :to="`/article/note/${item.id}`">
          <CardImgComponent :item="item" />
        </RouterLink>
      </div>
    </div>
    <div class="mt-2 md:mt-10" v-if="totalPages > 1">
      <PaginationComponent
        :current-page="pageStore.currentPage"
        :total-pages="totalPages"
        :page-range="5"
        @page-change="pageStore.handlePageChange"
      />
    </div>
  </div>
</template>
<style lang="less" scoped></style>
