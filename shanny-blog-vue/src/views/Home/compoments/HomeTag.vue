<script setup>
import TitleComponent from '@/views/_components/home/TitleComponent.vue'
import { useTagStore, useLanguageStore } from '@/stores'
import { onMounted } from 'vue'

const tagStore = useTagStore()
const languageStore = useLanguageStore()
const emit = defineEmits(['load-complete'])

onMounted(async () => {
  try {
    const res = await tagStore.getTagList()
    if (res.data.code.toLowerCase() === 'success') {
      tagStore.tags = res.data.data
    }
  } finally {
    emit('load-complete')
  }
})
</script>
<template>
  <div class="hidden md:block mt-10 w-full">
    <TitleComponent title="Tags" />
    <div class="mt-5 flex items-center flex-wrap text-sm/8">
      <a
        class="flex items-center mr-3 hover:cursor-pointer"
        v-for="item in tagStore.tags"
        :href="`/tag/${item.id}/${item.name}`"
      >
        <div aria-label="status" class="status status-primary"></div>
        <span class="ml-2 hover:text-primary hover:pointer">
          {{ languageStore.isEnglish ? item.nameEn : item.name }}
        </span>
      </a>
    </div>
  </div>
</template>
<style lang="scss" scoped></style>
