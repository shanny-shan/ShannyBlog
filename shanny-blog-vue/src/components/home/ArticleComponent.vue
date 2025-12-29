<script setup>
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
    class="card bg-base-200 shadow-sm md:p-3 flex flex-col md:flex-row"
    :class="{
      'mt-3': index !== 0,
      'mt-3 md:mt-0': index === 0,
    }"
  >
    <figure class="w-full md:w-1/4">
      <img src="@/assets/images/avatar.jpg" alt="Movie" class="md:rounded-xl" />
    </figure>
    <div
      class="card-body w-full p-3 md:p-5 md:w-3/4 flex flex-col justify-between"
    >
      <div class="flex flex-row items-center justify-between md:justify-start">
        <div>
          <font-awesome-icon
            icon="fa-regular fa-calendar"
            class="text-primary"
          />
          <span class="ml-1 text-xs font-light text-neutral">{{
            item.date
          }}</span>
        </div>
        <div class="hidden md:block ml-3">
          <font-awesome-icon icon="fa-solid fa-bell" class="text-primary" />
          <span class="ml-1 text-xs font-light text-neutral"
            >{{ item.readCount }} Read</span
          >
        </div>
        <div class="flex items-center ml-3">
          <font-awesome-icon icon="fa-solid fa-award" class="text-primary" />
          <div class="flex items-center">
            <span class="ml-1 text-xs font-light text-neutral">Rating: </span>
            <div class="ml-1 mb-1 rating rating-xs">
              <input
                v-for="i in 5"
                disabled
                type="radio"
                :name="'rating-' + item.id + '_' + i"
                class="mask mask-star-2 bg-warning"
                aria-label="1 star"
                :checked="item.rate == i"
              />
            </div>
          </div>
        </div>
      </div>
      <div>
        <h2 class="card-title text-2xl">{{ item.title }}</h2>
        <p class="text-base mt-3">{{ item.content }}</p>
      </div>
      <div class="flex justify-between items-center">
        <div class="flex items-center">
          <a class="flex items-center mr-3">
            <div aria-label="status" class="status status-primary"></div>
            <span class="ml-1 text-xs">Vue</span>
          </a>
          <a class="flex items-center mr-3">
            <div aria-label="status" class="status status-warning"></div>
            <span class="ml-1 text-xs">CSS</span>
          </a>
        </div>
        <div class="flex justify-end items-center">
          <img src="@/assets/images/avatar.jpg" class="w-5 rounded-full" />
          <span class="ml-1 text-xs">{{ item.author }}</span>
        </div>
      </div>
    </div>
  </div>
</template>
<style lang="scss" scoped></style>
