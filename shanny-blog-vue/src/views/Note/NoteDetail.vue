<script setup>
import { ref, onMounted, onUnmounted } from 'vue'
import DetailMessage from '@/views/_components/common/DetailMessage.vue'
import { useScrollStore, useSiteStore } from '@/stores'
import { useRoute } from 'vue-router'
import { useArticleStore } from '@/stores/modules/article'
import { formatDateTime } from '@/utils/time'
const articleStore = useArticleStore()
const siteStore = useSiteStore()

const route = useRoute()
const id = route.params.id
const scrollStore = useScrollStore()
const article = ref({})

const getArticleDetail = async () => {
  const res = await articleStore.getArticleByIds(id)
  if (res.data.code.toLowerCase() == 'success') {
    article.value = res.data.data
    siteStore.loading = false
  }
}

onMounted(() => {
  siteStore.loading = true
  scrollStore.enableScrollListener()
  getArticleDetail()
})
onUnmounted(() => {
  scrollStore.disableScrollListener()
})
</script>
<template>
  <div
    class="mt-18 md:mt-35 flex flex-col items-center w-full md:w-7/10 p-2 md:p-0"
    :class="scrollStore.isScrolled ? 'md:mt-35' : ''"
  >
    <div
      class="flex flex-col md:flex-row items-center md:items-start w-full mt-2 md:mt-10"
      :class="
        scrollStore.isScrolled
          ? 'md:relative md:justify-end'
          : 'md:justify-between'
      "
    >
      <div
        class="w-full md:w-1/4 flex flex-col items-center md:items-start pr-0 md:pr-5"
        :class="scrollStore.isScrolled ? 'md:sticky md:top-35 md:left-0' : ''"
      >
        <DetailMessage :item="article" />
      </div>
      <div class="w-full md:w-3/4 pl-0 md:pl-5 mt-3 md:mt-0">
        <div class="w-full p-3 md:p-10 bg-base-200 shadow-md rounded-2xl">
          <!-- 文章标题 -->
          <div class="flex flex-col md:flex-row-reverse items-center w-full">
            <div class="w-full md:w-1/3">
              <img :src="article.image" class="w-full rounded-2xl" />
            </div>
            <div
              class="w-full md:w-3/4 flex flex-col items-center justify-center mt-3 md:mt-0"
            >
              <div class="font-bold text-2xl md:text-3xl text-center">
                {{ article.title }}
              </div>
              <div class="flex flex-row items-center mt-5">
                <font-awesome-icon
                  icon="fa-regular fa-calendar"
                  class="text-primary text-sm"
                />
                <div class="flex flex-row items-center ml-2 text-sm">
                  <span class="font-bold">Published:</span>
                  <span class="ml-2">{{
                    formatDateTime(article.createTime)
                  }}</span>
                </div>
              </div>
            </div>
            s
          </div>
          <!-- 文章内容 -->
          <div class="mt-5 md:mt-10">
            <v-md-preview
              :text="article.content"
              class="text-lg leading-10 break-words"
            />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<style lang="less" scoped></style>
