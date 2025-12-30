<script setup>
import { onMounted, ref } from 'vue'
import ArticleComponent from '@/components/home/ArticleComponent.vue'
import TitleComponent from '@/components/home/TitleComponent.vue'
import { useArticleStore } from '@/stores/modules/article'
const articleStore = useArticleStore()
const articleList = ref([])
const getArticleList = async () => {
  const res = await articleStore.getArticles()
  if(res.data.code.toLowerCase() === 'success'){
    articleList.value = res.data.data
  }
}
onMounted(() => {
  getArticleList()
})
</script>
<template>
  <div class="block md:hidden mt-5">
    <TitleComponent title="Articles" />
  </div>
  <div v-for="(item, index) in articleList" :key="index">
    <ArticleComponent :item="item" :index="index" />
  </div>
</template>
<style lang="scss" scoped></style>
