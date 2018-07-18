(function (app) {
    app.controller('productListController', productListController);

    productListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function productListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        //search function
        $scope.search = search;
        function search() {
            getProducts();
        }

        //get list product
        $scope.products = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.keyword = '';
        $scope.getProducts = getProducts;
        function getProducts(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            };
            apiService.get('/api/product/getall', config, function (result) {
                if (result.data.TotalCount === 0) {
                    notificationService.displayWarning('Không tìm thấy dữ liệu!');
                }
                else {
                    $scope.products = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pagesCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount;
                }
            }, function (error) {
                notificationService.displayError('Không tải được dữ liệu');
                console.log(error);
            });
        }

        //select all function
        $scope.isAll = false;
        $scope.selectAll = selectAll;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.products, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.products, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        //select a record
        $scope.$watch('products', function (n, o) {
            var checked = $filter('filter')(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        //delete function
        $scope.deleteProduct = deleteProduct;
        function deleteProduct(id) {
            $ngBootbox.confirm('Bạn muốn xóa sản phẩm này?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.del('/api/product/delete', config, function success(result) {
                    notificationService.displaySuccess('Xóa sản phẩm  ' + result.data.Name + ' thành công');
                    getProducts();
                }, function error(err) {
                    notificationService.displayError('Xóa sản phẩm không thành công');
                    console.log(err);
                });
            });
        }

        //delete multiple record
        $scope.deleteMultiple = deleteMultiple;
        function deleteMultiple() {
            var listID = [];
            $.each($scope.selected, function (i, item) {
                listID.push(item.ID);
            });
            $ngBootbox.confirm('Bạn muốn xóa ' + listID.length + ' sản phẩm').then(function () {
                var config = {
                    params: {
                        checkedProducts: JSON.stringify(listID)
                    }
                };
                apiService.del('/api/product/deletemulti', config, function success(result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi');
                    getProducts();
                }, function error(err) {
                    notificationService.displayError('Xóa sản phẩm không thành công');
                    console.log(err);
                });
            });
        }

        //execution
        $scope.getProducts();
    }
})(angular.module('onlineshop.products'));