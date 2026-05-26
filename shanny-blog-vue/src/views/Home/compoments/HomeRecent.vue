<script setup>
import { onMounted, ref } from 'vue'
import RecentComponent from '@/components/home/RecentComponent.vue'
import TitleComponent from '@/components/home/TitleComponent.vue'
import { useArticleStore } from '@/stores/modules/article'
import { getTypePath } from '@/config/enum'
const articleStore = useArticleStore()
const articleList = ref([])
const getArticleList = async () => {
  const res = await articleStore.getArticles()
  if (res.data.code.toLowerCase() === 'success') {
    articleList.value = res.data.data
  }
}
onMounted(() => {
  getArticleList()
})
</script>
<template>
  <div class="hidden md:block mt-5 md:mt-10 w-full">
    <TitleComponent title="Recent Posts" />
    <div class="mt-3 md:mt-5">
      <div v-for="(item, index) in articleList" :key="index">
        <RouterLink :to="`${getTypePath(item.type)}/${item.id}`">
          <RecentComponent :item="item" :index="index" />
        </RouterLink>
      </div>
    </div>
  </div>
</template>
<style lang="scss" scoped></style>
