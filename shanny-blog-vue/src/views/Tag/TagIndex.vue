<script setup>
import { onMounted, onUnmounted } from 'vue'
import { useScrollStore, usePageStore, useArticleStore } from '@/stores'
import { RouterLink } from 'vue-router'
import CardImgComponent from '@/views/_components/common/CardImgComponent.vue'
import PaginationComponent from '@/views/_components/common/PaginationComponent.vue'
import { useRoute } from 'vue-router'

const scrollStore = useScrollStore()
const pageStore = usePageStore()
const articleStore = useArticleStore()

const route = useRoute()
const tagId = route.params.tagId
const tagName = route.params.tagName

const { pageList, totalPages } = pageStore.getPageData(
  () => articleStore.tagList,
)

onMounted(async () => {
  scrollStore.enableScrollListener()
  await articleStore.getTagArticleList(tagId)
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
    <div class="text-primary font-bold text-2xl">Tag / {{ tagName }}</div>
    <div
      class="flex flex-col md:flex-row md:flex-wrap md:justify-start items-center w-full mt-2 md:mt-10"
    >
      <div v-for="item in pageList" :key="item.id" class="w-full md:w-1/4 p-2">
        <RouterLink
          :to="`/article/${item.type == 'ARTICLE_NOTE' ? 'note' : 'project'}/${item.id}`"
        >
          <CardImgComponent :item="item" :index="index" />
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
