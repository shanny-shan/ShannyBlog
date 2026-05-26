<script setup>
import TitleComponent from '@/components/home/TitleComponent.vue'
import { useTagStore } from '@/stores/modules/tag'
import { useLanguageStore } from '@/stores/modules/language'
import { onMounted } from 'vue'
const tagStore = useTagStore()
const languageStore = useLanguageStore()
onMounted(async () => {
  const res = await tagStore.getTagList()
  if (res.data.code.toLowerCase() === 'success') {
    tagStore.tags = res.data.data
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
