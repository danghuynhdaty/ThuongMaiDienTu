(function (app) {
    app.controller('productAddController', productAddController);

    productAddController.$inject = ['$scope', 'notificationService', 'commonService', 'apiService', '$state'];

    function productAddController($scope, notificationService, commonService, apiService, $state) {
        $scope.product = {};

        //2 ways binding for seo title
        $scope.getSeoTitle = getSeoTitle;

        //add product
        $scope.addProduct = addProduct;

        function addProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.post('/api/product/create', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới');
                $state.go('products');
            }, function (error) {
                noitificationService.displayError('Thêm sản phẩm thất bại');
            });
        }

        //2 ways binding for alias
        $scope.getSeoTitle = getSeoTitle;
        function getSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        //get list product category
        function getProductCategories() {
            apiService.get('/api/product/getproductcategories', null, function (result) {
                $scope.productCategories = result.data;
            }, function (error) {
                if (error.status !== 401) {
                    notificationService.displayError('Không tải được danh mục sản phẩm');
                }
                console.log(error);
            });
        }

        //ckeditor config
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '300px'
        };

        //choose image
        $scope.chooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                });
            }
            finder.popup();
        }

        //choose more images
        $scope.moreImages = [];
        $scope.chooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                });
            };
            finder.popup();
        }

        //execution
        getProductCategories();
    }
})(angular.module('onlineshop.products'));