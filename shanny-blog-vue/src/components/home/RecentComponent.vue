<script setup>
import { formatDateTime } from '@/utils/time'
defineProps({
  index: {
    type: Number,
  },
  item: {
    type: Object,
    default: () => {},
  },
})
</script>
<template>
  <div
    class="flex justify-between items-center hover:cursor-pointer"
    :class="index == 0 ? '' : 'mt-4'"
  >
    <div class="w-1/5 md:w-1/3">
      <img :src="item.image" class="w-full rounded-md" />
    </div>
    <div class="flex flex-col justify-between w-4/5 md:w-2/3 ml-2 md:ml-4">
      <div class="hover:underline">
        <a href="#" class="text-sm font-bold">{{ item.title }}</a>
      </div>
      <div class="flex items-center mt-2">
        <font-awesome-icon icon="fa-regular fa-calendar" class="text-primary" />
        <span class="ml-2 text-xs font-light text-neutral">{{
          formatDateTime(item.updateTime)
        }}</span>
      </div>
    </div>
  </div>
</template>
<style lang="scss" scoped></style>
