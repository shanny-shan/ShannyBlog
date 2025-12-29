<script setup>
import { ref, computed, onMounted, onUnmounted } from 'vue'
import DetailMessage from '@/components/common/DetailMessage.vue'

import { useScrollStore } from '@/stores/modules/scroll'
import { useRoute } from 'vue-router'
const scrollStore = useScrollStore()
const route = useRoute()
const type = route.params.type
const id = route.params.id
onMounted(() => {
  scrollStore.enableScrollListener()
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
        <DetailMessage />
        <!-- <DetailTable /> -->
      </div>
      <div class="w-full md:w-3/4 pl-0 md:pl-5 mt-3 md:mt-0">
        <div class="w-full p-3 md:p-10 bg-base-200 shadow-md rounded-2xl">
          <!-- 文章标题 -->
          <div class="flex flex-col items-center w-full mt-5 md:mt-10">
            <div
              class="w-full flex flex-col items-center justify-center mt-3 md:mt-0"
            >
              <div class="font-bold text-2xl md:text-4xl text-center">
                Far far away, behind the word mountains
              </div>
              <div class="flex flex-row items-center mt-5">
                <font-awesome-icon
                  icon="fa-regular fa-calendar"
                  class="text-primary text-sm"
                />
                <div class="flex flex-row items-center ml-2 text-sm">
                  <span class="font-bold">Published:</span>
                  <span class="ml-2">September 26, 2019</span>
                </div>
              </div>
            </div>
          </div>
          <!-- 文章内容 -->
          <div class="mt-5 md:mt-10">
            <div class="text-lg leading-10">
              Far far away, behind the word mountains, far from the countries
              Vokalia and Consonantia, there live the blind texts.<br />

              Separated they live in Bookmarksgrove right at the coast of the
              Semantics, a large language ocean.<br />

              A small river named Duden flows by their place and supplies it
              with the necessary regelialia. It is a paradisematic country, in
              which roasted parts of sentences fly into your mouth.<br />
              Far far away, behind the word mountains, far from the countries
              Vokalia and Consonantia, there live the blind texts.<br />

              Separated they live in Bookmarksgrove right at the coast of the
              Semantics, a large language ocean.<br />

              A small river named Duden flows by their place and supplies it
              with the necessary regelialia. It is a paradisematic country, in
              which roasted parts of sentences fly into your mouth.<br />
              Far far away, behind the word mountains, far from the countries
              Vokalia and Consonantia, there live the blind texts.<br />

              Separated they live in Bookmarksgrove right at the coast of the
              Semantics, a large language ocean.<br />

              A small river named Duden flows by their place and supplies it
              with the necessary regelialia. It is a paradisematic country, in
              which roasted parts of sentences fly into your mouth.<br />
              Far far away, behind the word mountains, far from the countries
              Vokalia and Consonantia, there live the blind texts.<br />

              Separated they live in Bookmarksgrove right at the coast of the
              Semantics, a large language ocean.<br />

              A small river named Duden flows by their place and supplies it
              with the necessary regelialia. It is a paradisematic country, in
              which roasted parts of sentences fly into your mouth.<br />
              Far far away, behind the word mountains, far from the countries
              Vokalia and Consonantia, there live the blind texts.<br />

              Separated they live in Bookmarksgrove right at the coast of the
              Semantics, a large language ocean.<br />

              A small river named Duden flows by their place and supplies it
              with the necessary regelialia. It is a paradisematic country, in
              which roasted parts of sentences fly into your mouth.<br />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<style lang="less" scoped></style>
