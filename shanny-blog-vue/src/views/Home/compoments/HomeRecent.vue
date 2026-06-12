<script setup>
import { onMounted, ref } from 'vue'
import RecentComponent from '@/views/_components/home/RecentComponent.vue'
import TitleComponent from '@/views/_components/home/TitleComponent.vue'
import { useArticleStore } from '@/stores'
import { getTypePath } from '@/config/enum'

const articleStore = useArticleStore()
const recentList = ref([])
const emit = defineEmits(['load-complete'])

const getRecentList = async () => {
  try {
    const res = await articleStore.getRecents()
    if (res.data.code.toLowerCase() === 'success') {
      recentList.value = res.data.data
    }
  } finally {
    emit('load-complete')
  }
}
onMounted(async () => {
  await getRecentList()
})
</script>
<template>
  <div class="hidden md:block mt-5 md:mt-10 w-full">
    <TitleComponent title="Recent Posts" />
    <div class="mt-3 md:mt-5">
      <div v-for="(item, index) in recentList" :key="index">
        <RouterLink :to="`${getTypePath(item.type)}/${item.id}`">
          <RecentComponent :item="item" :index="index" />
        </RouterLink>
      </div>
    </div>
  </div>
</template>
<style lang="scss" scoped></style>
