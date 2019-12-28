package com.tef.zuulproxy;

public class ItemReport {
    private Integer inWarehouse;
    private Integer inOrder;
    private Double percentInOrder;

    public ItemReport() {
        inWarehouse = 0;
        inOrder = 0;
        percentInOrder = 0.0;
    }

    public Integer getInWarehouse() {
        return inWarehouse;
    }

    public void setInWarehouse(Integer inWarehouse) {
        this.inWarehouse = inWarehouse;
    }

    public Integer getInOrder() {
        return inOrder;
    }

    public void setInOrder(Integer inOrder) {
        this.inOrder = inOrder;
    }

    public Double getPercentInOrder() {
        return percentInOrder;
    }

    public void setPercentInOrder(Double percentInOrder) {
        this.percentInOrder = percentInOrder;
    }
}
