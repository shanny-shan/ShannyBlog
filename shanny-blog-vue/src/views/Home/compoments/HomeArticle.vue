<script setup>
import { ref } from 'vue'
import ArticleComponent from '@/components/home/ArticleComponent.vue'
import TitleComponent from '@/components/home/TitleComponent.vue'
const items = ref([
  {
    id: 1,
    title: 'The spectacle before us was indeed sublime',
    content:
      'Welcome, it’s great to have you here. We know that first impressions are important, so we’ve populated your new site…',
    date: ' September 26, 2019',
    tag: ['Life', 'Music', 'Art'],
    readCount: 12,
    rate: 4,
    author: 'Shanny',
  },
  {
    id: 2,
    title: 'Far far away, behind the word mountains',
    content:
      'Far far away, behind the word mountains, far from the countries Vokalia and Consonantia, there live the blind texts. Separated…',
    date: 'August 15, 2019',
    tag: ['Music', 'Art'],
    readCount: 160,
    rate: 3,
    author: 'Shanny',
  },
  {
    id: 3,
    title: 'The meaning of health has evolved over time',
    content:
      'In keeping with the biomedical perspective, early definitions of health focused on the theme of the body’s ability to function;…',
    date: 'July 26, 2019',
    tag: ['Art'],
    readCount: 3,
    rate: 2,
    author: 'Shanny',
  },
  // {
  //   id: 4,
  //   title: 'Musical improvisation is the spontaneous music',
  //   content:
  //     'It was one of the worst storms to hit London since God knows when. The thunder rolled, lightning flashed and…',
  //   date: 'July 26, 2019',
  //   tag: ['Life', 'Music'],
  //   readCount: 150,
  //   rate: 5,
  //   author: 'Shanny',
  // },
  // {
  //   id: 5,
  //   title: 'Customizing your brand and design settings',
  //   content:
  //     'The Ghost editor has everything you need to fully optimise your content. This is where you can add tags and…',
  //   date: 'July 26, 2019',
  //   tag: ['Music'],
  //   readCount: 10,
  //   rate: 3,
  //   author: 'Shanny',
  // },
])
</script>
<template>
  <div class="block md:hidden mt-5">
    <TitleComponent title="Articles" />
  </div>
  <div v-for="(item, index) in items" :key="index">
    <ArticleComponent :item="item" :index="index" />
  </div>
</template>
<style lang="scss" scoped></style>
