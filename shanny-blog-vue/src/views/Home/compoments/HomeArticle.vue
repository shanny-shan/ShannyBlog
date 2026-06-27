<script setup>
import { onMounted } from 'vue'
import ArticleComponent from '@/views/_components/home/ArticleComponent.vue'
import TitleComponent from '@/views/_components/home/TitleComponent.vue'
import { useArticleStore } from '@/stores'
import { getTypePath } from '@/config/enum'

const articleStore = useArticleStore()

const emit = defineEmits(['load-complete'])

onMounted(async () => {
  try {
    await articleStore.getViewList()
  } finally {
    emit('load-complete')
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
