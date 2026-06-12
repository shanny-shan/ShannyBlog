<script setup>
defineProps({
  id: {
    type: String,
  },
  items: {
    type: Array,
    default: () => [],
  },
})
</script>
<template>
  <div class="drawer">
    <input :id="id" type="checkbox" class="drawer-toggle" />
    <div class="drawer-content">
      <label :for="id" class="btn btn-square btn-ghost">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          class="h-5 w-5"
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M4 6h16M4 12h16M4 18h7"
          />
        </svg>
      </label>
    </div>
    <div class="drawer-side">
      <label
        :for="id"
        aria-label="close sidebar"
        class="drawer-overlay"
      ></label>
      <ul class="menu bg-base-100 w-70">
        <template v-for="(item, index) in items" :key="index">
          <li v-if="!item.children">
            <RouterLink
              :to="item.path"
              class="active-bg-primary hover-bg-primary"
              >{{ item.title }}</RouterLink
            >
          </li>
          <li v-else>
            <h2 class="menu-title">{{ item.title }}</h2>
            <ul>
              <li v-for="(child, cIndex) in item.children" :key="cIndex">
                <RouterLink
                  :to="child.path"
                  class="active-bg-primary hover-bg-primary"
                  >{{ child.title }}</RouterLink
                >
              </li>
            </ul>
          </li>
        </template>
      </ul>
    </div>
  </div>
</template>
<style lang="scss" scoped></style>
