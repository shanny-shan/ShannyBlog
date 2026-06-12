<script setup>
import { formatDateTime } from '@/utils/time'
import defaultImage from '@/assets/images/loading.gif'
defineProps({
  index: {
    type: Number,
  },
  item: {
    type: Object,
    default: () => {},
  },
  footer: {
    type: Boolean,
    default: true,
  },
})

const handleLoad = (e, item) => {
  if (!e.target.dataset.done) {
    e.target.dataset.done = '1'
    e.target.src = item.image
  }
}

const handleError = (e) => {
  e.target.dataset.done = '1'
  e.target.src = defaultImage
}
</script>
<template>
  <div class="card bg-base-200 shadow-sm flex w-full">
    <figure class="w-full">
      <img
        :src="defaultImage"
        @load="(e) => handleLoad(e, item)"
        @error="handleError"
      />
    </figure>
    <div class="card-body w-full p-3 flex flex-col justify-between">
      <div>
        <h2 class="card-title text-2xl">{{ item.title }}</h2>
        <p class="text-base mt-3 truncate">{{ item.content }}</p>
      </div>
      <div class="flex justify-between items-center mt-2" v-if="footer">
        <div class="flex justify-end items-center">
          <img :src="item.href" class="w-5 rounded-full" />
          <span class="ml-1 text-xs">Shanny</span>
        </div>
        <div class="flex align-center">
          <font-awesome-icon icon="fa-regular fa-eye" class="text-primary" />
          <span class="ml-1 text-xs font-light text-neutral">
            {{ item.view > 99 ? '99+' : item.views }}
          </span>
        </div>
        <div class="flex align-center">
          <font-awesome-icon
            icon="fa-regular fa-calendar"
            class="text-primary"
          />
          <span class="ml-1 text-xs font-light text-neutral">{{
            formatDateTime(item.updateTime)
          }}</span>
        </div>
      </div>
    </div>
  </div>
</template>
<style lang="scss" scoped></style>
