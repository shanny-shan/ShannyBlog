<script setup>
import { ref } from 'vue'
import TitleComponent from '@/components/home/TitleComponent.vue'
import RecentComponent from '@/components/home/RecentComponent.vue'
const items = ref([
  {
    id: 1,
    title: 'The spectacle before us was indeed sublime',
    date: ' September 26, 2019',
  },
  {
    id: 2,
    title: 'Far far away, behind the word mountains',
    date: 'August 15, 2019',
  },
  {
    id: 3,
    title: 'The meaning of health has evolved over time',
    date: 'July 26, 2019',
  },
])
</script>
<template>
  <div class="hidden md:block mt-5 md:mt-10 w-full">
    <TitleComponent title="Recent Posts" />
    <div class="mt-3 md:mt-5">
      <div v-for="(item, index) in items" :key="index">
        <RecentComponent :item="item" :index="index" />
      </div>
    </div>
  </div>
</template>
<style lang="scss" scoped></style>
