<script setup>
const year = new Date().getFullYear()
</script>
<template>
  <footer
    class="footer footer-horizontal footer-center bg-base-200 text-base-content rounded p-5 md:p-15 mt-5 md:mt-10"
  >
    <div class="md:w-7/10">
      <nav>
        <div class="grid grid-flow-col gap-4">
          <a
            href="https://github.com/shanny-shan?tab=repositories"
            target="_blank"
          >
            <font-awesome-icon
              icon="fa-brands fa-github"
              class="text-2xl md:text-3xl hover:text-primary"
            />
          </a>
          <a
            class="ml-4"
            href="https://space.bilibili.com/335353354?spm_id_from=333.1007.0.0"
            target="_blank"
          >
            <font-awesome-icon
              icon="fa-brands fa-bilibili"
              class="text-2xl md:text-3xl icon-bilibili"
            />
          </a>
          <a class="ml-4" href="/" target="_blank">
            <font-awesome-icon
              icon="fa-brands fa-steam"
              class="text-2xl md:text-3xl icon-steam hover:text-primary"
            />
          </a>
        </div>
      </nav>
      <aside class="mt-3 md:mt-5">
        <p class="text-xs md:text-base">
          Copyright © {{ year }} - All right reserved by ACME Industries Ltd
        </p>
      </aside>
    </div>
  </footer>
</template>

<style lang="scss" scoped></style>
