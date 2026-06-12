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
  <div class="card bg-base-200 shadow-sm flex w-full">
    <div
      class="card-body w-full pt-8 pb-8 pl-5 pr-5 flex flex-col justify-between"
    >
      <!-- <div class="flex justify-end items-center">
        <font-awesome-icon
          icon="fa-solid fa-award"
          class="text-primary text-sm"
        />
        <div class="flex items-center ml-1">
          <div class="text-sm">Defficuly:</div>
          <div class="ml-2 rating rating-xs">
            <input
              v-for="i in 5"
              disabled
              type="radio"
              :name="'rating-' + item.id + '-' + i"
              class="mask mask-star-2 bg-warning"
              aria-label="1 star"
              :checked="item.rate == i"
            />
          </div>
        </div>
      </div> -->
      <div>
        <h2 class="card-title text-2xl text-center mt-5">{{ item.title }}</h2>
        <p class="text-base mt-5 text-center truncate">{{ item.content }}</p>
      </div>
      <div class="flex justify-center items-center mt-5">
        <div class="flex items-center">
          <img :src="item.href" class="w-5 rounded-full" />
          <span class="ml-1 text-xs">Shanny</span>
        </div>
        <div class="ml-3">
          <font-awesome-icon icon="fa-regular fa-eye" class="text-primary" />
          <span class="text-xs font-light text-neutral">
            {{ item.view > 99 ? '99+' : item.views }}
          </span>
        </div>
        <div class="ml-3">
          <font-awesome-icon
            icon="fa-regular fa-calendar"
            class="text-primary"
          />
          <span class="text-xs font-light text-neutral">{{
            formatDateTime(item.updateTime)
          }}</span>
        </div>
      </div>
    </div>
  </div>
</template>
<style lang="scss" scoped></style>
