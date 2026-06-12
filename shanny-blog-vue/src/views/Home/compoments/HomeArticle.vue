<script setup>
import { onMounted, ref } from 'vue'
import ArticleComponent from '@/views/_components/home/ArticleComponent.vue'
import TitleComponent from '@/views/_components/home/TitleComponent.vue'
import { useArticleStore } from '@/stores'
import { getTypePath } from '@/config/enum'

const articleStore = useArticleStore()
const viewList = ref([])
const emit = defineEmits(['load-complete'])

const getViewList = async () => {
  try {
    const res = await articleStore.getViews()
    if (res.data.code.toLowerCase() === 'success') {
      viewList.value = res.data.data
    }
  } finally {
    emit('load-complete')
  }
}
onMounted(async () => {
  await getViewList()
})
</script>
<template>
  <div class="block md:hidden mt-5">
    <TitleComponent title="Articles" />
  </div>
  <div v-for="(item, index) in viewList" :key="index">
    <a :href="`${getTypePath(item.type)}/${item.id}`" class="cursor-pointer">
      <ArticleComponent :item="item" :index="index" />
    </a>
  </div>
</template>
<style lang="scss" scoped></style>
