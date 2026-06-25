<script setup>
import { onMounted, ref } from 'vue'
import ArticleComponent from '@/views/_components/home/ArticleComponent.vue'
import TitleComponent from '@/views/_components/home/TitleComponent.vue'
import { useArticleStore, useSiteStore } from '@/stores'
import { getTypePath } from '@/config/enum'

const articleStore = useArticleStore()
const siteStore = useSiteStore()

onMounted(async () => {
  try {
    await articleStore.getViewList()
  } finally {
    siteStore.handleLoadComplete()
  }
})
</script>
<template>
  <div class="block md:hidden mt-5">
    <TitleComponent title="Articles" />
  </div>
  <div v-for="(item, index) in articleStore.viewList" :key="index">
    <a :href="`${getTypePath(item.type)}/${item.id}`" class="cursor-pointer">
      <ArticleComponent :item="item" :index="index" />
    </a>
  </div>
</template>
<style lang="scss" scoped></style>
