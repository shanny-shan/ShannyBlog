<script setup>
import { computed } from 'vue'

const props = defineProps({
  // 当前页码
  currentPage: {
    type: Number,
    default: 1,
  },
  // 总页数
  totalPages: {
    type: Number,
    default: 100,
  },
  // 显示的页码数量
  pageRange: {
    type: Number,
    default: 5,
  },
})

const emit = defineEmits(['pageChange'])

// 计算应该显示的页码数组
const visiblePages = computed(() => {
  const pages = []
  const halfRange = Math.floor(props.pageRange / 2)

  // 处理总页数少于显示范围的情况
  if (props.totalPages <= props.pageRange) {
    for (let i = 1; i <= props.totalPages; i++) {
      pages.push(i)
    }
    return pages
  }

  let startPage, endPage

  // 处理当前页靠近首页的情况
  if (props.currentPage <= halfRange + 1) {
    startPage = 1
    endPage = props.pageRange
  }
  // 处理当前页靠近末页的情况
  else if (props.currentPage >= props.totalPages - halfRange) {
    startPage = props.totalPages - props.pageRange + 1
    endPage = props.totalPages
  }
  // 处理当前页在中间的情况
  else {
    startPage = props.currentPage - halfRange
    endPage = props.currentPage + halfRange

    // 确保范围对称
    if (props.pageRange % 2 === 0) {
      endPage--
    }
  }

  // 添加开始的页码
  if (startPage > 1) {
    pages.push(1)
    if (startPage > 2) {
      pages.push('ellipsis')
    }
  }

  // 添加中间的页码
  for (let i = startPage; i <= endPage; i++) {
    pages.push(i)
  }

  // 添加结束的页码
  if (endPage < props.totalPages) {
    if (endPage < props.totalPages - 1) {
      pages.push('ellipsis')
    }
    pages.push(props.totalPages)
  }

  return pages
})

// 处理页码点击事件
const handlePageClick = (page) => {
  if (page === 'ellipsis' || page === props.currentPage) return
  emit('pageChange', page)
}

// 处理上一页点击事件
const handlePrevPage = () => {
  if (props.currentPage > 1) {
    emit('pageChange', props.currentPage - 1)
  }
}

// 处理下一页点击事件
const handleNextPage = () => {
  if (props.currentPage < props.totalPages) {
    emit('pageChange', props.currentPage + 1)
  }
}

// 处理第一页点击事件
const handleFirstPage = () => {
  if (props.currentPage !== 1) {
    emit('pageChange', 1)
  }
}

// 处理最后一页点击事件
const handleLastPage = () => {
  if (props.currentPage !== props.totalPages) {
    emit('pageChange', props.totalPages)
  }
}
</script>

<template>
  <div class="join flex justify-center items-center my-8">
    <!-- 第一页按钮 -->
    <button
      class="join-item btn btn-square"
      :disabled="currentPage === 1"
      @click="handleFirstPage"
      aria-label="First page"
    >
      <font-awesome-icon icon="fa-solid fa-angle-double-left" />
    </button>

    <!-- 上一页按钮 -->
    <button
      class="join-item btn btn-square"
      :disabled="currentPage === 1"
      @click="handlePrevPage"
      aria-label="Previous page"
    >
      <font-awesome-icon icon="fa-solid fa-angle-left" />
    </button>

    <!-- 页码按钮 -->
    <template v-for="page in visiblePages" :key="page">
      <button
        v-if="page === 'ellipsis'"
        class="join-item btn btn-disabled"
        disabled
      >
        ...
      </button>
      <button
        v-else
        class="join-item btn btn-square"
        :class="page === currentPage ? 'bg-primary text-primary-content' : ''"
        @click="handlePageClick(page)"
        :aria-label="`Page ${page}`"
        :aria-current="page === currentPage ? 'page' : undefined"
      >
        {{ page }}
      </button>
    </template>

    <!-- 下一页按钮 -->
    <button
      class="join-item btn btn-square"
      :disabled="currentPage === totalPages"
      @click="handleNextPage"
      aria-label="Next page"
    >
      <font-awesome-icon icon="fa-solid fa-angle-right" />
    </button>

    <!-- 最后一页按钮 -->
    <button
      class="join-item btn btn-square"
      :disabled="currentPage === totalPages"
      @click="handleLastPage"
      aria-label="Last page"
    >
      <font-awesome-icon icon="fa-solid fa-angle-double-right" />
    </button>
  </div>
</template>

<style lang="scss" scoped>
.join {
  display: inline-flex;
  flex-wrap: wrap;
  gap: 0;
}

.join-item:not(:first-child) {
  border-top-left-radius: 0 !important;
  border-bottom-left-radius: 0 !important;
}

.join-item:not(:last-child) {
  border-top-right-radius: 0 !important;
  border-bottom-right-radius: 0 !important;
}

/* 自定义按钮样式 */
.btn:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}
</style>
