#pragma once

template<class T>
struct aligned_allocator
{
    using value_type = T;

    constexpr static std::align_val_t align = std::align_val_t(alignof(T) * 8);

    aligned_allocator() = default;

    template<class Alloc>
    aligned_allocator(const Alloc&) {};

    template<class Alloc>
    bool operator==(const Alloc&) const { return true; }

    T* allocate(size_t n)
    {
        return (T*)::operator new(n * sizeof(float), align);
    }

    void deallocate(void* ptr, size_t n)
    {
        ::operator delete(ptr, align);
    }
};